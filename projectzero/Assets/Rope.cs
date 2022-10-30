using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSeglen = 0.25f;
    private float segmentLenght = 35;
    private int lineWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<lineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i= 0; i < segmentLenght; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSeglen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
        
    }

    private void DrawRope()
    {
        float LineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLenght];
        for (int i =0; i< this.segmentLenght; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.setPositions(ropePositions);
    }

}
