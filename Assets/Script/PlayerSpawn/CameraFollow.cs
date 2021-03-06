using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    //[SerializeField] private Transform followTarget;
    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        instance = this;
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        //fcam = GetComponent<CinemachineFreeLook>();
    }

    public void ChangeTarget(Transform newTarget)
    {
        vCam.Follow = newTarget; 
    }

}
