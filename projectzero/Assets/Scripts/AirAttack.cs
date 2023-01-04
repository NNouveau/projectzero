using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public CharacterCombat playerCombat;
    public Rigidbody2D rb;

    [Header("Variables")]
    public float arrowSpeed;
    public float arrowDamage;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerCombat = player.GetComponent<CharacterCombat>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, -30));
        rb.transform.rotation = rotation;
        rb.velocity = new Vector3(transform.position.x, -transform.position.y, transform.position.z) * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name!="TreeArcher")
        {
            if (collision.tag == "Player")
            {
                playerCombat.takeDamage(arrowDamage);
            }
        }
        Destroy(gameObject);
    }
}
