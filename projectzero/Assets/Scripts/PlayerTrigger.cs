using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    public QOTACombat qotaCombat;

    private void Awake()
    {
        qotaCombat = GetComponentInParent<QOTACombat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="Player")
        {
            qotaCombat.healthBar.gameObject.SetActive(true);
        }
    }
}
