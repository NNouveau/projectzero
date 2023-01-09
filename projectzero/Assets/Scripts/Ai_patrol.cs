using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_patrol : MonoBehaviour
{
    public float walkSpeed = -40f;
    public bool mustPatrol;
    public bool mustFlip;
    public Animator animator;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask ground;
    public Collider2D bodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            animator.SetBool("canWalk",true);
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position,2f,ground);
        }
    }

    void Patrol()
    {
        if (mustFlip||bodyCollider.IsTouchingLayers(ground))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed*Time.fixedDeltaTime,rb.velocity.y);
    }
    void Flip()
    {
        mustPatrol = false;
        animator.SetBool("canWalk", false);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
        animator.SetBool("canWalk", true);
    }
    
    
   
}
