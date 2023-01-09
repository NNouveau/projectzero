using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Animator animator;
    public Transform arrowPoint;
    public Transform beamPoint;
    public Transform airArrowPoint;
    public GameObject arrowPrefab;
    public GameObject player;
    public GameObject arrowRainPrefab;
    public GameObject ultimateAttackPrefab;
    public GameObject airAttackPrefab;
    public Rigidbody2D rb;
    public float arrowRainCastTime;
    public float rangedAttackCastTime;
    public float ultimateAttackCastTime;
    public float airAttackCastTime;
    public float airJumpStr;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(IErangedAttack());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            arrowRain();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(IErultimateAttack());
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(IErairAttack());
        }

    }

    private IEnumerator IErangedAttack()
    {
        animator.SetTrigger("doRangedAttack");
        yield return new WaitForSeconds(rangedAttackCastTime);
        Instantiate(arrowPrefab, arrowPoint.transform.position, arrowPoint.transform.rotation);
    }
    private IEnumerator IErultimateAttack()
    {
        animator.SetTrigger("doUltimateAttack");
        yield return new WaitForSeconds(ultimateAttackCastTime);
        Instantiate(ultimateAttackPrefab, beamPoint.transform.position, beamPoint.transform.rotation);
    }
    private IEnumerator IErairAttack()
    {
        rb.AddForce(Vector2.up * airJumpStr, ForceMode2D.Impulse);
        animator.SetTrigger("doAirAttack");
        yield return new WaitForSeconds(airAttackCastTime);
        Instantiate(airAttackPrefab, airArrowPoint.transform.position, airArrowPoint.transform.rotation);
    }
    public void arrowRain()
    {
        StartCoroutine(IEarrowRainStart());
    }
    private IEnumerator IEarrowRainStart()
    {
        animator.SetTrigger("doArrowRain");
        Vector3 arrowRainPosition = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
        yield return new WaitForSeconds(arrowRainCastTime);
        Instantiate(arrowRainPrefab, arrowRainPosition, player.transform.rotation);
    }
    
}
