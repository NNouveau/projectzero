using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public CharacterCombat playerCombat;
    public Rigidbody2D rb;

    [Header("Variables")]
    public float beamSpeed;
    public float beamDamage;
    public float beamDecayDuration;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerCombat = player.GetComponent<CharacterCombat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * beamSpeed;
        StartCoroutine(IErultimateAttackDecay());
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name!="TreeArcher")
        {
            if (collision.tag == "Player")
            {
                playerCombat.takeDamage(beamDamage);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator IErultimateAttackDecay()
    {
        yield return new WaitForSeconds(beamDecayDuration);
        Destroy(gameObject);
    }
}
