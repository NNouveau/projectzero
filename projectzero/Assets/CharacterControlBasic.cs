using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterControlBasic : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    //public Animator animator;
    
    public LayerMask groundLayer;

    //Variables for Rope

    public Rigidbody2D rbfr;
    private HingeJoint2D hj;
    public float RopePushForce= 10f;
    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;
    public GameObject pulleySelected = null;
    
    //Code for rope mechanic

    void Awake()
    {
        rbfr = gameObject.GetComponent<Rigidbody2D>();
        hj   = gameObject.GetComponent<HingeJoint2D>();
    }
        


    
    //Codes and variables for character mechanic

    [Header("Movements")]
    public float runSpeed = 8f, jumpStr = 10f, fallMultiplier = 5f, gravity = 1f, jumpDelay = 0.25f,ACC = 9f,DCC =9f, velPower =1.2f;
    private float jumpTimer;
    private bool face = true;
    public Vector3 velocity;
    public Vector2 direction;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLenght = 1f;
    public Vector3 colliderOffset;

    [Header("Physics")]
    public float linearDrag = 4f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);
        if (Input.GetKey(KeyCode.W))
        {
            //jumpTimer = Time.time + jumpDelay;
        }

        CheckKeyboardInputs();
        //CheckPulleyInputs();

    }

    //Codes for rope mechanic

    void CheckKeyboardInputs()
    {
        if(Input.GetKey("a") || Input.GetKey("left"))
        {
            if(attached)
            {
                rbfr.AddRelativeForce(new Vector3(-1, 0, 0) * RopePushForce);
            }
        }
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            if(attached)
            {
                rbfr.AddRelativeForce(new Vector3(1, 0, 0) * RopePushForce);
            }
        }

        if((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && attached)
        {
            Slide(1);
        }

        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && attached)
        {
            Slide(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Detach();
        }
    }

    public  void  Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }

    void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled=false;
        hj.connectedBody=null;
    }

    public void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;

        if(direction>0)
        {
            if(myConnection.connectedAbove != null)
            {
                if(myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }
        else
        {
            if(myConnection.connectedBelow != null)
            {
                newSeg =myConnection.connectedBelow;
            }
        }
    }

    void OnTriggerEnter2d(Collider2D collision)
    {
        if(!attached)
        {
            if(collision.gameObject.tag == "Rope")
            {
                if(attachedTo != collision.gameObject.transform.parent)
                {
                    if(disregard == null || collision.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }

  /*  void CheckPulleyInputs()
    {
        if(Input.GetMouseButtonDown(0))
        {

        }
    }
  */

    //For some optimization
    private void FixedUpdate()
    {
        velocity.x = rb.velocity.x; //Will be removed
        velocity.y = rb.velocity.y; //Will be removed

        modifyPhysics();
        run(direction.x);
        if (Input.GetKey(KeyCode.W) && onGround) //(jumpTimer > Time.time && onGround)
        {
            jump();
        }
        //animator.SetFloat("VerticalSpeed", rb.velocity.y);
        if (onGround)
        {
            //animator.SetBool("isJumping", false);
        }
        else if (!onGround)
        {
            //animator.SetBool("isJumping", true);
        }
    }


    //Running function
    public void run(float horizontal)
    {
        float targetSpeed    = horizontal * runSpeed;
        float speedDif       = targetSpeed - rb.velocity.x;
        float accelRate      = (Mathf.Abs(targetSpeed) > 0.00f) ? ACC : DCC;
        float movement       = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);

        
        //animator.SetFloat("HorizontalSpeed", Mathf.Abs(velocity.x));
        if ((horizontal > 0 && !face) || (horizontal < 0 && face))
        {
            flip();
        }
    }


    //Jumping function
    public void jump()
    {
        //animator.SetTrigger("jumped");
        //animator.SetBool("isJumping", true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStr, ForceMode2D.Impulse);
        jumpTimer = 0;
    }


    //For better jumping
    void modifyPhysics()
    {
        if (onGround)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if ((rb.velocity.y > 0) && !Input.GetKey(KeyCode.W))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }


    //Fliping character function
    void flip()
    {
        face = !face;
        transform.Rotate(0f, 180f, 0f);
    }


    //Drawing hitbox function
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLenght);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLenght);
    }
}
