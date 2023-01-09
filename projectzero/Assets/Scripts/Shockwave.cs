using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [Header("Shockwave")]
    [SerializeField]
    bool isMaxSize;
    float timer;
    public Material shockwaveMat;
    public float shockwaveRadiusGrowth;
    public float shockwaveMaxSize;
    Vector2 startScale;
    Vector2 maxScale;

    [Header("Components")]
    public GameObject player;
    public CharacterCombat characterCombat;
    public NecroCombat necroCombat;

    private void Awake()
    {
        player = GameObject.Find("Player");
        characterCombat = player.GetComponent<CharacterCombat>();
        necroCombat = GameObject.FindGameObjectWithTag("Necromancer").GetComponent<NecroCombat>();
    }
    void Start()
    {
        isMaxSize = false;
        timer = 0f;
        startScale = transform.localScale;
        maxScale = new Vector2(shockwaveMaxSize, shockwaveMaxSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMaxSize)
        {
            StartCoroutine(shockwave());
            if (transform.localScale.x >= shockwaveMaxSize)
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
    }
    private IEnumerator shockwave()
    {
         yield return new WaitForSeconds(0.1f);
         transform.localScale = Vector3.Lerp(startScale, maxScale, timer / shockwaveRadiusGrowth);
         timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = necroCombat.shockwaveDamage;
        if (collision.gameObject.tag=="Player"&&player.tag!="Invincible")
        {
            characterCombat.takeDamage(damage);
        }
    }

}
