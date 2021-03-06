using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeeSawPlatform : MonoBehaviour
{
    [SerializeField] private bool UseMotor;
    [SerializeField] private int velocityThreshold; 
    private int startVelocity;
    private float currentAngle, currentVelocity;
    private JointMotor motor;
    private HingeJoint hingeJointProperties;
    private bool canAssignMotor;
    void Awake()
    {
        if(UseMotor)
        {
            startVelocity = UnityEngine.Random.Range(-velocityThreshold, velocityThreshold + 1);
            hingeJointProperties = GetComponent<HingeJoint>();
            hingeJointProperties.useMotor = true;
            motor = hingeJointProperties.motor;
            motor.targetVelocity = startVelocity;
            hingeJointProperties.motor = motor;
            currentVelocity = motor.targetVelocity;
        }
    }


    void Update()
    {
        if (UseMotor)
            ChangeVelocityDirection();
    }

    private void ChangeVelocityDirection()
    {
        currentAngle = hingeJointProperties.angle;

        if (currentVelocity == Math.Abs(startVelocity) && currentAngle >= 39)
        {
            ChangeDirection();
        }
        else if (currentVelocity == -Math.Abs(startVelocity) && currentAngle <= -39)
        {
            ChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        if(canAssignMotor&&UseMotor)
        {
            motor.targetVelocity = currentVelocity;
            hingeJointProperties.motor = motor;
            canAssignMotor = false;
        }
        
    }

    void ChangeDirection()
    {
        currentVelocity *= -1;
        canAssignMotor = true;
    }
}
