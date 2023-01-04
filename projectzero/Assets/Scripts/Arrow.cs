using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public CharacterMovements playerMovements;
    public CharacterCombat playerCombat;
    public Rigidbody2D rb;
    public GameObject rootEffect;
    public SpriteRenderer spr;
    public GameObject bow;
    

    [Header("Variables")]
    public float arrowSpeed;
    public float arrowDamage;
    public float rootEffectDuration;
    public bool canRooted;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        playerMovements = player.GetComponent<CharacterMovements>();
        playerCombat = player.GetComponent<CharacterCombat>();
        bow = GameObject.Find("TreeArcher");
    }
    void Start()
    {
        rb.velocity = transform.right * arrowSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            playerCombat.takeDamage(arrowDamage);
            bow.GetComponent<Bow>().arrowRain();
            if (canRooted)
            {
                canRooted = false;
                GameObject a = Instantiate(rootEffect, transform.position, transform.rotation) as GameObject;
                Destroy(a, rootEffectDuration);
                playerMovements.isRooted = true;
                StartCoroutine(IErootDecay());
            }
        }
        
        spr.enabled = false;
    }
    
    private IEnumerator IErootDecay()
    {
        yield return new WaitForSeconds(rootEffectDuration);
        playerMovements.isRooted = false;
        Destroy(gameObject);
        canRooted = true;
    }

    
    // Update is called once per frame

}
