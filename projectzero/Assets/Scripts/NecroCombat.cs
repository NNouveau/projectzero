using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroCombat : Combat
{ 
    [Header("Cooldowns")]
    [SerializeField]
    int healingCD;
    [SerializeField] protected int spawnSkelCD;
    [SerializeField] protected int shockwaveCD;
    [SerializeField] bool canSpawn=false;
    [SerializeField] bool canShockwave = true;
    public float shockwaveDamage=150;
    


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
        //if (canSpawn)
        //{
        //    necroAnimator.SetTrigger("spawnSkeletons");
        //    StartCoroutine(IEspawnSkeletons(skeletons));
        //    canSpawn = false;
        //    StartCoroutine(IEspawnSkeletonCD());
        //}

        if (canShockwave)
        {
            necroAnimator.SetTrigger("attack");
            StartCoroutine(IEspawnShockWave());
            canShockwave = false;
            StartCoroutine(IEshockwaweCD());
        }
    }

    private IEnumerator IEspawnSkeletons(GameObject[] skeletons)
    {
        Debug.Log("çalýþtým");
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

    private IEnumerator IEspawnShockWave()
    {
        yield return new WaitForSeconds(shockCastTime);
        GameObject a = Instantiate(shockWave) as GameObject;
        a.transform.position = new Vector3(transform.position.x+ 0.142f, transform.position.y+ 0.112f, transform.position.z);
    }

    private IEnumerator IEspawnSkeletonCD()
    {
        yield return new WaitForSeconds(spawnSkelCD);
        canSpawn = true;
    }
    private IEnumerator IEshockwaweCD()
    {
        yield return new WaitForSeconds(shockwaveCD);
        canShockwave = true;
    }
}
