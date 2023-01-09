using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public float speed;          // Speed value for platform
    public int startingPoint;    // Starting position of the platform
    public Transform[] points;   // An array for transform points

    private int i;               // will be using for index of array
    
    void Start()
    {
        transform.position = points[startingPoint].position; // Deciding which point you want to start
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if (i == points.Length)   // checking points to avoid  passing points.
            {
                i = 0;              // reset the index
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
