using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNormal : MonoBehaviour
{
    [Header("Variables")]
    public float arrowSpeed;
    public float arrowDamage;

    [Header("Components")]
    public GameObject player;
    public Rigidbody2D rb;
    public CharacterCombat playerCombat;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerCombat = player.GetComponent<CharacterCombat>();
    }
    void Start()
    {
        rb.velocity = transform.right * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerCombat.takeDamage(arrowDamage);
            Destroy(gameObject);
        }
    }
}
