using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashPow;
    [SerializeField] private float dashTime;
    [SerializeField] private int maxDash;
    [SerializeField] private int leftDash;
    Movements mv;
    TrailRenderer Playertr;
    Rigidbody2D playerrb;

    bool isDashing;
    bool canDash;
    Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        Playertr = GetComponent<TrailRenderer>();
        mv = GetComponent<Movements>();
    }

    // Update is called once per frame
    void Update()
    { 
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (direction==Vector2.zero)
        {
            direction = new Vector2(transform.localScale.x, 0);
        }
        if (mv.onGround)
        {
            leftDash = maxDash;
        }
        canDash = leftDash > 0;
        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            dash(direction);
        }
    }

    void dash(Vector2 direction)
    {
        isDashing = true;
        Playertr.emitting = true;
        playerrb.velocity = direction.normalized * dashPow;
        leftDash -= 1;
        StartCoroutine(stopDashing());
        return;
    }

    private IEnumerator stopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        Playertr.emitting = false;
        isDashing = false;
        playerrb.velocity = Vector2.zero;
    }
}
