using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CM : MonoBehaviour
{
    public Rigidbody rb;
    public WheelCollider lfW, rfW, lbW, rbW;
    public float driveSpeed, steerSpeed;
    float hInput, vInput;
    float mult;
    bool isRunning;

    private void Start()
    {
        isRunning = true;
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            Debug.Log(vInput);
            (hInput, vInput) = Update();
            ForwardMovement(vInput);
            SideWaysMovement(hInput);
        }
    }

    private (float,float) Update()
    {
        return (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void ForwardMovement(float vInput)
    {
        float motor = vInput * driveSpeed;
        float brake = driveSpeed*3;

        if (vInput != 0) // Aplica motor torque si hay entrada vertical
        {
            Accelerate(motor);
        }
        else // Si no hay entrada vertical, aplica frenado
        {
            Stop(brake);
        }
    }
    private void SideWaysMovement(float hInput)
    {
        lfW.steerAngle = steerSpeed * hInput;
        rfW.steerAngle = steerSpeed * hInput;
    }

    private void Accelerate(float motor)
    {
        lfW.motorTorque = motor * Time.timeScale;
        rfW.motorTorque = motor * Time.timeScale;
        lbW.motorTorque = motor * Time.timeScale;
        rbW.motorTorque = motor * Time.timeScale;

        // Desactiva el frenado mientras hay movimiento
        lfW.brakeTorque = 0;
        rfW.brakeTorque = 0;
        lbW.brakeTorque = 0;
        rbW.brakeTorque = 0;
    }

    private void Stop(float motor)
    {
        float brakeTorque = Mathf.Abs(motor);
        lfW.brakeTorque = brakeTorque * Time.timeScale;
        rfW.brakeTorque = brakeTorque * Time.timeScale;
        lbW.brakeTorque = brakeTorque * Time.timeScale;
        rbW.brakeTorque = brakeTorque * Time.timeScale;

        // Detén el motor torque para evitar conflictos
        lfW.motorTorque = 0;
        rfW.motorTorque = 0;
        lbW.motorTorque = 0;
        rbW.motorTorque = 0;
    }
}