using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPlatform : MonoBehaviour
{
    private Rigidbody playerObject;
    //Somethime you need refer by other object
    [SerializeField] private Transform startPoint,endPoint;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float pushSpeed , maxFanSpeed ,distanceMultiplier=30f;
    private float distance;

    private void Start()
    {
        direction = transform.TransformDirection(direction);
    }

    private void Update()
    {
        Debug.DrawLine(startPoint.transform.position, endPoint.transform.position, Color.green);
    }

    private void OnTriggerStay(Collider other)
    {
        IFanpPlatform fanplat = other.GetComponent<IFanpPlatform>();
        if (fanplat != null)
        {
            float fanDistance = calculateFanForce(other.transform.position);
            Vector3 eForce = direction * (pushSpeed-distance* distanceMultiplier) * Time.deltaTime;

            // DO clamping if using add force
            fanplat.ApplyFanForce(eForce,maxFanSpeed);
            
        }
    }

    float calculateFanForce(Vector3 objectTransform)
    {
        distance = Vector3.Distance(objectTransform, startPoint.transform.position);
        //Debug.Log("Distance : " + distance);
        return distance;
    }

    private void OnTriggerExit(Collider other)
    {
        IFanpPlatform fanplat = other.GetComponent<IFanpPlatform>();
        if (fanplat != null)
        {
            distance = 0;
        }
    }
}
