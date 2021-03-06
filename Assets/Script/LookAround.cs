using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{

    public static bool cursorLocked;
    public Transform orientation;
    public Transform cams;
    public Transform playerTransform;
    private bool resetCamera;
    float resetDirection;

    //private bool isWallLeftRefer, isWallRightRefer, isWallRunningRefer;
    //Look attribute
    [SerializeField] private float xSensitivity, ySensitivity, maxAngle = 80;
    [SerializeField] private float desiredX,xRotation;
    [SerializeField] private float maxWallRunCameraTilt, wallRunCameraTilt;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.5f, 0.5f);

    //Quaternion camCenter;

    void Awake()
    {
        cursorLocked = true;
    }

    void Update()
    {
        //SetY();
        //SetX();
        
        UpdateCursorLock();
        ResetCamera();
        
    }
    private void LateUpdate()
    {
        //SetXY();
        //transform.position = playerTransform.transform.position + offset;
    }

    //void SetY()//Up and down, rotation in x
    //{
    //    float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;//get input from player
    //    Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);//calculate angle axis with player input
    //    Quaternion t_delta = cams.localRotation * t_adj;//multiply the angle axis with camera current rotation



    //    if(Quaternion.Angle(camCenter,t_delta)<maxAngle)//calculate the rate of change of angle with origin and compare
    //    {
    //        cams.localRotation = t_delta;//apply calculated angle to the camera
    //        //cams.transform.localRotation = Quaternion.Euler(t_delta.x, cams.rotation.y, cams.rotation.z);
    //    }
    //}

    //void SetX()//Left and right , rotation in y
    //{
    //    float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;//get input from player
    //    Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);//calculate angle axis with player input
    //    Quaternion t_delta = orientation.localRotation * t_adj;//multiply the angle axis with camera current rotation

    //    orientation.localRotation = t_delta;//apply calculated angle to the camera
    //    //cams.transform.localRotation = Quaternion.Euler(cams.rotation.x, t_delta.y, cams.rotation.z);
    //}

    void SetXY()
    {
        //Get input
        float y_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;//get input from player
        float x_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;//get input from player

        //Vector3 rotationVector = cams.transform.localRotation.eulerAngles;
        //desiredX = rotationVector.y + x_input;

        //Rotate, and also make sure we dont over- or under-rotate.
        //xRotation -= y_input;
        //xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);

        //Perform the rotations
        //cams.transform.localRotation = Quaternion.Euler(xRotation, desiredX, wallRunCameraTilt);
        //orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);



        ////If the walltilt not reach the maximum and is wallrunning
        //if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunningRefer && isWallRightRefer)//Repeated
        //    wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;//add tilt
        //if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunningRefer && isWallLeftRefer)//Repeated
        //    wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;

        ////Tilts camera back again
        //if (wallRunCameraTilt > 0 && !isWallRightRefer && !isWallLeftRefer)
        //    wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;//reset back to normal tilt
        //if (wallRunCameraTilt < 0 && !isWallRightRefer && !isWallLeftRefer)
        //    wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
        
    }

    //Public Function
    public void wallRunCamera(float directionSign)
    {
        if(Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt)
        {
            wallRunCameraTilt += (Time.deltaTime * maxWallRunCameraTilt * 2)*directionSign;
            resetCamera = false;
            //Debug.Log("Wall Run Camera get Called and reset Camera : " + resetCamera);
        }
    }
 
    public void wallRunCameraReset(float directionSign)
    {
        resetCamera = true;
        resetDirection = -directionSign;
    }

    //Still need improve//Repeated//Cannot use !=0 
    private void ResetCamera()
    {
        if (wallRunCameraTilt >0 && resetCamera)
        {
            //Debug.Log("Reset Camera get Called and reset Camera 1 (Right Side): " + resetCamera);
            wallRunCameraTilt += (Time.deltaTime * maxWallRunCameraTilt * 2) * resetDirection;
            if(wallRunCameraTilt<=0)
            {
                resetCamera = false;
                wallRunCameraTilt = 0;

            }
        }
        else if(wallRunCameraTilt < 0 && resetCamera)
        {
            //Debug.Log("Reset Camera get Called and reset Camera (Left Wall): " + resetCamera);
            wallRunCameraTilt += (Time.deltaTime * maxWallRunCameraTilt * 2) * resetDirection;
            if(wallRunCameraTilt >= 0)
            {
                resetCamera = false;
                wallRunCameraTilt = 0;
                
            }
        }

    }


    void UpdateCursorLock()
    {
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }


}
