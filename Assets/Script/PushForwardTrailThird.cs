using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushForwardTrailThird : MonoBehaviour
{
    private Rigidbody rbThird;
    [SerializeField] private float pushSpeed;
    void Start()
    {
        rbThird = FindObjectOfType<PlayerMovementThird>().GetComponent<Rigidbody>();
        
    }


    //private void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Collided");
    //        rb.AddForce(Vector3.forward * pushSpeed, ForceMode.Impulse);
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided");
            rbThird.AddForce(Vector3.forward * pushSpeed*Time.fixedDeltaTime, ForceMode.Impulse);
            
        }
    }

    
}
