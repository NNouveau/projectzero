using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyMovements : EnemyMovements
{
   
    public bool canWalk;
    
    void Update()
    {
        if (canWalk)
        {
            followPlayer();
        }
    }
}
