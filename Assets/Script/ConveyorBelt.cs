using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Rigidbody playerObject;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float pushSpeed;
    
   

    private void OnTriggerStay(Collider other)
    {
        IConveyBelt conveyBelt = other.GetComponent<IConveyBelt>();
        if(conveyBelt!=null)
        {
            Vector3 eForce = transform.TransformDirection(direction) * pushSpeed;
            conveyBelt.ApplyConveyorForce(eForce);

            //isOnConveyBelt = true;
        }
    }



    //private void OnTriggerExit(Collider other)
    //{
    //    IConveyBelt conveyBelt = other.GetComponent<IConveyBelt>();
    //    if (conveyBelt!=null)
    //    {
    //        //conveyBelt.ApplyConveyorForce(-direction, deaccerationSpeed);
    //        conveyBelt.ResetExternalForce();
            
    //        //isOnConveyBelt = false;
    //    }
    //}


}
