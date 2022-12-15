using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : Movements
{
    public GameObject player;
    [SerializeField] protected float agroRange;
    float distanceToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        face = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer<agroRange)
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
    private void FixedUpdate()
    {
        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.y;
    }
}
