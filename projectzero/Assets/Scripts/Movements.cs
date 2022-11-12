
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
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float jumpStr;
    [SerializeField] protected float fallMultiplier;
    [SerializeField] protected float gravity;
    [SerializeField] protected float jumpDelay;
    [SerializeField] protected float ACC;
    [SerializeField] protected float DCC;
    [SerializeField] protected float velPower;

    
    //private float jumpTimer;
    private bool face = true;
    public Vector3 velocity;
    public Vector2 direction;

    [Header("Collision")]
    public bool onGround = false;
    [SerializeField] protected float groundLenght;
    public Vector3 colliderOffset;

    [Header("Physics")]
    public float linearDrag = 4f;


    


    //For some optimization
    


    //Running function
    public void run(float horizontal)
    {
        float targetSpeed = horizontal * runSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.00f) ? ACC : DCC;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);

        animator.SetFloat("HorizontalSpeed", Mathf.Abs(velocity.x));
        if ((horizontal > 0 && !face) || (horizontal < 0 && face))
        {
            flip();
        }
    }

    //Fliping character function
    public void flip()
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
