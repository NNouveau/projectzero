using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovements : EnemyMovements
{
    private void Update()
    {
        followPlayer();
    }
}
