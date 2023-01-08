using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniArcherCombat : Combat
{
    [Header("Components")]
    public Animator animator;
    public GameObject arrowPrefab;
    public GameObject player;
    public Transform arrowPoint;
    public MiniArcherMovements archerMovements;
    public LayerMask raycastmask;
    public Transform raycast;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;

    [Header("Variables")]
    public float raycastLenght;
    public float attackDistance;//minimum distance to attack
    public float attackRate;
    public bool canAttack;
    public float attackCastTime;
    public float intTimer;
    public bool attackMode = false, inRange, cooling;
    public float horizontal;

    // Start is called before the first frame update
    private void Awake()
    {
        archerMovements = GetComponent<MiniArcherMovements>();
        animator = GetComponent<Animator>(); 
        intTimer = attackRate;
        player=GameObject.Find("Player");
    }

    private void Update()
    {
        hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLenght, raycastmask);
        hit2 = Physics2D.Raycast(raycast.position, Vector2.right, raycastLenght, raycastmask);
        
        if (hit.collider != null)
        {
            miniArcherLogic();
        }
        else if (hit2.collider != null)
        {
            miniArcherLogic();
        }
        horizontal = player.transform.position.x - transform.position.x;

        if (horizontal<0&&!archerMovements.isCloseUp)
        {
            archerMovements.face = true;
        }
        else if (horizontal>0&& !archerMovements.isCloseUp)
        {
            archerMovements.face = false;
        }
        
    }

    void attack()
    {
        intTimer = attackRate;
        attackMode = true;
        animator.SetBool("isAttacking", true);
        Instantiate(arrowPrefab, arrowPoint.transform.position, arrowPoint.transform.rotation);
    }

    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("isAttacking", false);
    }

    void miniArcherLogic()
    {
        if (archerMovements.isCloseUp)
        {
            stopAttack();
        }
        else if (cooling == false)
        {
            attack();
        }
        if (cooling)
        {
            cooldown();
            animator.SetBool("isAttacking", false);
        }
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

    // Update is called once per frame





}
