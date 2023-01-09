using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombat : Combat
{
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name=="Player")
        {
            Combat player = collision.collider.GetComponent<Combat>();
            if (player != null)
            {
                player.takeDamage(attackDamage);
            }
        }
    }
}
