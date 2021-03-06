using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationThird : MonoBehaviour
{
    public Transform camTransform;
   
    
    void Update()
    {
        Debug.Log("Cam Rotation in Y-axis : " + camTransform.transform.rotation.y);
        transform.localRotation = Quaternion.Euler(0, camTransform.transform.rotation.y, 0);
        
    }
}
