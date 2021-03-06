using Cinemachine;
using UnityEngine;
using System;

public class CinemachinePOVExtension : CinemachineExtension
{
    public static bool cursorLocked;
    private Vector3 startRotatation;
    private float y_input, x_input, maxAngle = 70f;
    [SerializeField] private float xSensitivity, ySensitivity;

    private bool resetCamera;
    float resetDirection;

    //private bool isWallLeftRefer, isWallRightRefer, isWallRunningRefer;
    //Look attribute
    [SerializeField] private float maxWallRunCameraTilt;
    private float wallRunCameraTilt=0;


    private void Update()
    {
        y_input = Input.GetAxis("Mouse Y");//get input from player
        x_input = Input.GetAxis("Mouse X");//get input from player

        if(resetCamera)
        {
            ResetCamera();
        }
        
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage==CinemachineCore.Stage.Aim)
            {
                if (startRotatation == null) startRotatation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = new Vector2(x_input, y_input);
                startRotatation.x += deltaInput.x * xSensitivity * Time.deltaTime;
                startRotatation.y += deltaInput.y * ySensitivity * Time.deltaTime;
                startRotatation.y = Mathf.Clamp(startRotatation.y, -maxAngle, maxAngle);
                state.RawOrientation = Quaternion.Euler(-startRotatation.y, startRotatation.x, wallRunCameraTilt);
            }
        }
    }

    //Public Function
    public void wallRunCamera(float directionSign)
    {
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt)
        {
            wallRunCameraTilt += (Time.deltaTime * maxWallRunCameraTilt * 2) * directionSign;
            resetCamera = false;
            //Debug.Log("Wall Run Camera get Called and reset Camera : " + resetCamera);
        }
    }

    public void wallRunCameraReset()
    {
        resetCamera = true;
        //resetDirection = -directionSign;
    }

    //Still need improve//Repeated//Cannot use !=0 
    private void ResetCamera()
    {
        if (wallRunCameraTilt > 0 && resetCamera)
        {

            wallRunCameraTilt -= (Time.deltaTime * maxWallRunCameraTilt * 2);
            if (wallRunCameraTilt <= 0)
            {
                resetCamera = false;
                wallRunCameraTilt = 0;
                //Debug.Log("Reset Camera get Called and reset Camera 1 (Right Side): " + resetCamera);
            }
        }
        else if (wallRunCameraTilt < 0 && resetCamera)
        {
            wallRunCameraTilt += (Time.deltaTime * maxWallRunCameraTilt * 2);
            if (wallRunCameraTilt >= 0)
            {
                resetCamera = false;
                wallRunCameraTilt = 0;
                //Debug.Log("Reset Camera get Called and reset Camera (Left Wall): " + resetCamera);
            }
        }

    }

}
