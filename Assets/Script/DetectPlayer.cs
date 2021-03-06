using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    PlatformStaterGenerator ParentGenerator;
    Vector3 initialPosisition;
    int collideCount=0;
    float offset;
    int totalPlatformAmount;

    private void Start()
    {
        initialPosisition = transform.position;
        totalPlatformAmount = gameObject.GetComponentInParent<PlatformStaterGenerator>().getMaxPlatformAmount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&collideCount<totalPlatformAmount)
        {
            //Send msg to generator&activate platform
            offset = gameObject.GetComponentInParent<PlatformStaterGenerator>().PlayerCollided(collideCount);
            //Move forward
            transform.Translate(Vector3.forward * offset);
            
            //add count
            collideCount++;
        }
    }

    public void ResetDetector()
    {
        //Get back to parent posistion
        transform.position = initialPosisition;
        collideCount = 0;
    }

    
}
