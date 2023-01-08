using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyCombat : Combat
{
    [Header("Components")]
    public Animator animator;
    public Transform raycast;
    public GameObject target;
    public LayerMask raycastmask;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;
    public Transform hitbox;
    public MiniEnemyMovements enemyMovements;

    [Header("Public Variables")]
    public float raycastLenght;
    public float attackDistance;//minimum distance to attack
    public float attackRate;
    

    [Header("Private Variables")]
    public bool attackMode = false, inRange, cooling;
    public float distance;//store the distance between enemy and player
    private float intTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovements = GetComponent<MiniEnemyMovements>();
        enemyMovements.canWalk = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        intTimer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLenght, raycastmask);
            hit2 = Physics2D.Raycast(raycast.position, Vector2.right, raycastLenght, raycastmask);
            
            raycastDebugger();
        }
        //When player is detected
        if (hit.collider != null||hit2.collider!=null)
        {
            miniEnemyLogic();
        }
        else if (hit.collider == null|| hit2.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {
            stopAttack();
        }

    }

    protected virtual void attack()
    {
        intTimer = attackRate;
        attackMode = true;
        animator.SetBool("isAttacking", true);
        enemyMovements.canWalk = false;
    }

    void miniEnemyLogic()
    {
        distance = Mathf.Abs(Vector2.Distance(transform.position, target.transform.position));
        if (distance > attackDistance)
        {
            stopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            attack();
        }
        if (cooling)
        {
            cooldown();
            animator.SetBool("isAttacking", false);
        }
    }

    public void stopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("isAttacking", false);
        enemyMovements.canWalk = true;
    }

    void cooldown()
    {
        attackRate -= Time.deltaTime;
        if (attackRate <= 0 && cooling && attackMode)
        {
            cooling = false;
            attackRate = intTimer;
        }
    }

    public void raycastDebugger()
    {
        if (distance > attackDistance)
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
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }
    public void triggerCooling()
    {
        cooling = true;
    }
}
