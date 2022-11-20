using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroCombat : Combat
{ 
    [Header("Cooldowns")]
    [SerializeField] 
    int healingCD;
    int spawnSkelCD;
    int attackCD;
    


    [SerializeField] float spawnCastTime;
    [SerializeField] float shockCastTime;




    private Transform targetPlayer;
    
    public GameObject[] skeletons;
    public GameObject shockWave;
    public Animator necroAnimator;
    

    // Start is called before the first frame update
    void Start()
    {
        necroAnimator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            necroAnimator.SetTrigger("spawnSkeletons");
            StartCoroutine(spawnSkeletons(skeletons));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            necroAnimator.SetTrigger("attack");
            StartCoroutine(spawnShockWave());
        }
    }

    private IEnumerator spawnSkeletons(GameObject[] skeletons)
    {
        yield return new WaitForSeconds(spawnCastTime);
        GameObject a = Instantiate(skeletons[0]) as GameObject;
        GameObject b = Instantiate(skeletons[1]) as GameObject;
        GameObject c = Instantiate(skeletons[2]) as GameObject;
        GameObject d = Instantiate(skeletons[3]) as GameObject;
        a.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y - 1, transform.position.z);
        b.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        c.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y - 1, transform.position.z);
        d.transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
    }

    private IEnumerator spawnShockWave()
    {
        yield return new WaitForSeconds(shockCastTime);
        GameObject a = Instantiate(shockWave) as GameObject;
        a.transform.position = new Vector3(transform.position.x+ 0.142f, transform.position.y+ 0.112f, transform.position.z);
    }
}
