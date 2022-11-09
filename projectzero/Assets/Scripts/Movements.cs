using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;


    [Header("Movements")]
    public float runSpeed = 8f, jumpStr = 10f, fallMultiplier = 5f, gravity = 1f, jumpDelay = 0.25f;
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
        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.y;
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);
        if (Input.GetKey(KeyCode.W))
        {
            jumpTimer = Time.time + jumpDelay;
        }

    }


    //For some optimization
    private void FixedUpdate()
    {
        modifyPhysics();
        if (!gameObject.GetComponent<Abilities>().isAttached)
        {
            run(direction.x);
        }
        
        if (jumpTimer > Time.time&& onGround)
        {
            jump();
        }
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
        if (!onGround)
        {
            animator.SetBool("onGround", false);
        }
        else if (onGround)
        {
            animator.SetBool("onGround", true);
        }
    }


    //Running function
    public void run(float horizontal)
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * runSpeed * Time.deltaTime;
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(velocity.x));
        if ((horizontal > 0 && !face) || (horizontal < 0 && face))
        {
            flip();
        }
    }


    //Jumping function
    public void jump()
    {
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
            //rb.drag = linearDrag * 0.15f;
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
