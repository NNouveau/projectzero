using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : Movements
{
    public GameObject player;
    [SerializeField] protected float agroRange;
    public bool isCloseUp;
    
    float distanceToPlayer;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    protected void followPlayer()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < agroRange)
        {
            float horizontal = player.transform.position.x - transform.position.x;
            if (horizontal > 0)
            {
                horizontal = 1;
            }
            else if (horizontal < 0)
            {
                horizontal = -1;
            }
            run(horizontal);
        }
    }
    protected void keepDistance()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < agroRange)
        {
            float horizontal = player.transform.position.x - transform.position.x;
            isCloseUp = true;
            if (horizontal > 0)
            {
                horizontal = -1;
            }
            else if (horizontal < 0)
            {
                horizontal = 1;
            }
            run(horizontal);
        }
        else
        {
            isCloseUp = false;
        }
    }
   
}
