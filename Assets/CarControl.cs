using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
   private float horizontalInput, verticalInput;
   private float currentSteerAngle, currentbreakForce;
   private bool isBreaking;
   
   //settings
   [SerializeField] private float motorForce, breakForce, maxSteerAngle;

   //wheel colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    //wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        getInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void getInput()
    {
        //Steering
        horizontalInput = Input.GetAxis("Horizontal");

        //Acceleration
        verticalInput = Input.GetAxis("Vertical");

        //Breaking
        isBreaking = Input.GetKey(KeyCode.Space);

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce:0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
        
    }
    
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        frontLeftWheelTransform.Rotate(frontLeftWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        frontRightWheelTransform.Rotate(frontRightWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        rearLeftWheelTransform.Rotate(rearLeftWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        rearRightWheelTransform.Rotate(rearRightWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}   
