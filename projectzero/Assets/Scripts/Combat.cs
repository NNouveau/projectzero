using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float healingAmount;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float canGetDamageCD;
    [SerializeField] protected bool canGetDamage;

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
        if (canGetDamage)
        {
            canGetDamage = false;
            StartCoroutine(IEcanGetDamageCD());
            Debug.Log(dmg + " hasar alýndý");
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                die();
            }
        }
        
    }
    void healYourself(float healingAmount)
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
    void die()
    {
        Debug.Log("öldün");
    }
    private IEnumerator IEcanGetDamageCD()
    {
        yield return new WaitForSeconds(canGetDamageCD);
        canGetDamage = true;
    }
}
