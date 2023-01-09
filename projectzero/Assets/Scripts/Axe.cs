using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int axeDamage = 35;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player")
        {
            Combat player = hitInfo.GetComponent<Combat>();
            if (player != null)
            {
                player.takeDamage(axeDamage);
            }
            
        }

    }
}
