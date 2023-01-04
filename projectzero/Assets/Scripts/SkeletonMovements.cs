using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovements : EnemyMovements
{
    SkeletonCombat skeletonCombat;
    private void Start()
    {
        skeletonCombat = GetComponent<SkeletonCombat>();
    }
    void Update()
    {
        if (skeletonCombat.attackMode==false)
        {
            followPlayer();
        }
    }
}
