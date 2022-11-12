using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int healingAmount;
    [SerializeField] protected int damage;
    [SerializeField] protected int attackDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {

    }
    public void takeDamage(float dmg)
    {

    }
    void healYourself(int healingAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healingAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}
