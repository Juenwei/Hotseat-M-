using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkLine : MonoBehaviour
{
    private LineRenderer lineRender;
    private Transform startPoint, endPoint;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        lineRender.SetPosition(0, startPoint.position);
        lineRender.SetPosition(1, endPoint.position);
    }
}
