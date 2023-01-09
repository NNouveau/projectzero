using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : Movements
{
    public bool isRooted;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLenght = .2f;
    }

    void Update()
    {

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);
        if (Input.GetKey(KeyCode.W))
        {
            //jumpTimer = Time.time + jumpDelay;
        }
        if (isRooted)
        {
            rb.velocity = Vector2.zero;
        }

    }
    private void FixedUpdate()
    {

        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.y;

        modifyPhysics();
        if (!gameObject.GetComponent<Abilities>().isAttached&&!isRooted)
        {
            run(direction.x);
        }

        if (Input.GetKey(KeyCode.W) && onGround&&!isRooted) //(jumpTimer > Time.time && onGround)
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
    public void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStr, ForceMode2D.Impulse);
        //jumpTimer = 0;
    }
}
