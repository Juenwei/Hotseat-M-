using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private int pointNum = 0;
    private int totalPoint;
    [SerializeField] private List<Transform> endpoints;
    [SerializeField] private float tolerance, delayTime, speed;
    private Vector3 currentTarget;

    [Header("Total point include startPoint")]
    private Vector3[] vecPoint;

    private float delayStart;

    void Start()
    {
        totalPoint = endpoints.Count;
        vecPoint = new Vector3[totalPoint];
        //vecPoint[0] = transform.position;
        for (int i = 0; i < totalPoint; i++) 
        {
            vecPoint[i] = endpoints[i].position;
        }
        

        if (vecPoint.Length > 0)
        {
            currentTarget = vecPoint[0];
        }
        tolerance = speed * Time.deltaTime;
        
    }

    void Update()
    {
        if (transform.position != currentTarget)
        {
            movePlatform();
        }
        else
        {
            updateTarget();
        }
    }

    private void movePlatform()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    private void updateTarget()
    {
        if (Time.time - delayStart > delayTime)
        {
            nextPlatform();
        }
    }

    private void nextPlatform()
    {
        pointNum++;
        if (pointNum >= vecPoint.Length)
        {
            System.Array.Reverse(vecPoint);
            pointNum = 0;
        }
        currentTarget = vecPoint[pointNum];
    }
}
