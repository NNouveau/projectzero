using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform raycast;
    private GameObject target;
    public LayerMask raycastmask;
    private RaycastHit2D hit;
    public Transform hitbox;

    [Header("Public Variables")]
    public float raycastLenght;
    public float attackDistance;//minimum distance to attack
    public float attackRate;
    public int attackDamage = 35;

    

    [Header("Private Variables")]
    private bool attackMode=false,inRange,cooling;
    private float distance;//store the distance between enemy and player
    private float intTimer;
    private int maxHealt = 100;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        intTimer = attackRate;
        currentHealth = maxHealt;
        
    }

    private void Update()
    {

        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLenght, raycastmask);
            raycastDebugger();
        }
        //When player is detected
        if (hit.collider!=null)
        {
            enemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange==false)
        {
            Ai_patrol aipatrol = FindObjectOfType<Ai_patrol>();
            animator.SetBool("canWalk", true);
            aipatrol.mustPatrol = true;
            stopAttack();
        }
    }
    void enemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance)
        {
            move();
            stopAttack();
        }
        else if (attackDistance>=distance&&cooling==false)
        {
            attack();
        }
        if (cooling)
        {
            cooldown();
            animator.SetBool("Attack",false);
        }
    }
    void attack()
    {
        Ai_patrol aipatrol = FindObjectOfType<Ai_patrol>();
        intTimer = attackRate;
        attackMode = true;
        aipatrol.mustPatrol = false;
        animator.SetBool("canWalk", false);
        animator.SetBool("attack",true);

    }



    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("attack", false);
    }
    private void move()
    {
        Ai_patrol aipatrol = FindObjectOfType<Ai_patrol>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("skelAttack"))
        {
            aipatrol.mustPatrol = true;
        }
        
    }
    void cooldown()
    {
        attackRate -= Time.deltaTime;
        if (attackRate<=0&&cooling&&attackMode)
        {
            cooling = false;
            attackRate = intTimer;
        }
    }
    void raycastDebugger()
    {
        if (distance>attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLenght, Color.red);
        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLenght, Color.green);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag=="Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }
    public void triggerCooling()
    {
        cooling = true;
    }


    //Damage function
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        //hurt animation
        animator.SetTrigger("getDamage");
        if (currentHealth<=0)
        {
            Die();
        }
    }

    //Die function
    void Die()
    {
        //Stops when died
        Ai_patrol aipatrol = FindObjectOfType<Ai_patrol>();
        aipatrol.mustPatrol = false;
        //play die animation
        animator.SetBool("isDead",true);
        //Remove the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 2f);
    }
}
