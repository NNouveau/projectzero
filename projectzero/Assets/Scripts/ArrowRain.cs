using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public CharacterMovements playerMovements;
    public CharacterCombat playerCombat;

    public float arrowRainDecayDuration;
    public float arrowRainDamage;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerMovements = player.GetComponent<CharacterMovements>();
        playerCombat = player.GetComponent<CharacterCombat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEarrowRainDecay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator IEarrowRainDecay()
    {
        yield return new WaitForSeconds(arrowRainDecayDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerCombat.takeDamage(arrowRainDamage);
        }
    }
}
