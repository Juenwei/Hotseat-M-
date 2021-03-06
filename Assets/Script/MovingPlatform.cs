using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private int pointNum = 0;
    [SerializeField] private float tolerance, delayTime, speed;
    [SerializeField] private bool isReversable;
    private Vector3 currentTarget;
    private bool isPlayerEnter;
    private int totalPoint;
    [SerializeField] private List<Transform> moveEndpoints;
    [Header("Total point include startPoint")]
    private Vector3[] vecPoint;

    private float delayStart;


    void Start()
    {
        totalPoint = moveEndpoints.Count;
        vecPoint = new Vector3[totalPoint];
        //vecPoint[0] = transform.position;
        for (int i = 0; i < totalPoint; i++)
        {
            vecPoint[i] = moveEndpoints[i].position;
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

    private void OnTriggerEnter(Collider other)
    {
        IAttachableObject attachableObject = other.GetComponent<IAttachableObject>();
        if(attachableObject!=null)
        {
            attachableObject.AttachObject(transform);
            isPlayerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IAttachableObject attachableObject = other.GetComponent<IAttachableObject>();
        if (isPlayerEnter)
        {
            attachableObject.ReleaseObject(null);
            isPlayerEnter = false;
        }
    }
    
}
