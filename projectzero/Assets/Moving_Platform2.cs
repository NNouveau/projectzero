using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform2 : MonoBehaviour
{
    public Transform pos1, pos2, startpos;
    public float speed;
    Vector3 nextpos;
    void Start()
    {
        nextpos = startpos.position;
    }

    
    void Update()
    {
        if(transform.position== pos1.position)
        {
            nextpos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextpos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
