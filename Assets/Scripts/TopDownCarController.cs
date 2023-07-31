using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.2f;
    public float accelerationFactor = 15.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 8;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D carRigBody2D;

    void Awake()
    {
        carRigBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigBody2D.velocity);

        //Forward speed limit, cancel force if goes above max speed
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // Reverse speed limit, cancel force if goes above 50% max speed in reverse direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        // All direction speed limit, cancel forec if goes above in any direction
        if (carRigBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed & accelerationInput > 0)
        {
            return;
        }

        //reduce sliding force when no force is applied on model
        if (accelerationInput == 0)
        {
            carRigBody2D.drag = Mathf.Lerp(carRigBody2D.drag, 1.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigBody2D.drag = 0;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        carRigBody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigBody2D.velocity, transform.right);

        carRigBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigBody2D.velocity.magnitude;
    }


    float GetLateralVelocity() {
        return Vector2.Dot(transform.right, carRigBody2D.velocity);
    }

    public bool TireOnDrift(out float laterVel, out bool isBraking) {
        laterVel = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0) {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 4.0f) {
            return true;
        }

        return false;
    }

}
