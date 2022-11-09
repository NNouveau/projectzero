using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashPow;
    [SerializeField] private float dashTime;
    [SerializeField] private int maxDash;
    [SerializeField] private int leftDash;
    Movements mv;
    TrailRenderer Playertr;
    Rigidbody2D playerrb;

    bool isDashing;
    bool canDash;
    Vector2 direction;

    [Header("SwingRope")]
    [SerializeField] private float pushPow;
    [SerializeField] private float detachPow;
    public bool isAttached = false;
    private GameObject disregard;
    private HingeJoint2D hj;
    public Transform attachedTo;
    



    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        Playertr = GetComponent<TrailRenderer>();
        mv = GetComponent<Movements>();
        hj = gameObject.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (direction==Vector2.zero)
        {
            direction = new Vector2(transform.localScale.x, 0);
        }
        if (mv.onGround)
        {
            leftDash = maxDash;
        }
        canDash = leftDash > 0&&!isAttached;
        if (canDash && Input.GetKeyDown(KeyCode.F))
        {
            dash(direction);
        }
        swingRope(direction);
    }

    void dash(Vector2 direction)
    {
        isDashing = true;
        Playertr.emitting = true;
        leftDash -= 1;
        addVelocity(direction,dashPow);
        return;
    }

    private IEnumerator stopVelocity()
    {
        yield return new WaitForSeconds(dashTime);
        Playertr.emitting = false;
        isDashing = false;
        playerrb.velocity = Vector2.zero;
    }

    void addVelocity(Vector2 direction,float power)
    {
        playerrb.velocity = direction.normalized * power;
        StartCoroutine(stopVelocity());
    }

    void swingRope(Vector2 direction)
    {
        if (isAttached)
        {
            playerrb.AddRelativeForce(new Vector3(direction.x,0,0)*pushPow);
            if (Input.GetKeyDown(KeyCode.W))
            {
                slide(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                slide(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                detach(direction);
            }
        }
       

    }

    void slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (direction==1&& myConnection.connectedAbove != null)
        {
            if (myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
            {
                newSeg = myConnection.connectedAbove;
            }
        }
        else if (myConnection.connectedBelow!=null)
        {
            newSeg = myConnection.connectedBelow;
        }
        if (newSeg!=null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
    void detach(Vector2 direction)
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        addVelocity(direction,detachPow);
        isAttached = false;
        hj.enabled = false;
        hj.connectedBody = null;
    }
    void attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached=true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        isAttached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttached&&collision.gameObject.tag=="Rope")
        {
            if (true)
            {
                attach(collision.gameObject.GetComponent<Rigidbody2D>());
            }
            //if (attachedTo!=collision.gameObject.transform.parent||true) ayný ipe bir kez tutunmamýzý saðlýyor
            //disregard == null || collision.gameObject.transform.parent.gameObject != disregard
        }
    }
}
