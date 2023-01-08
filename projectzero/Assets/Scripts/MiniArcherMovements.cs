using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniArcherMovements : MiniEnemyMovements
{
    // Update is called once per frame
    void Update()
    {
        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.y;
        keepDistance();
    }
}
