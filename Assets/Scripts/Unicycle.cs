using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unicycle : MonoBehaviour {
    
    [Header("Tilt")]
    public float tiltingForce = 2;
    public Transform visualV, visualU;
    public float tiltU;
    public float tiltV;
    public float gravityTiltEffect = 2;
    public float naturalGravity = 1;
    public float maxTiltSpeed = 0.04f;
    
    [Header("Movement")]
    public float movementForce = 1;
    public float maxSpeed = 5;
    public Rigidbody rb;
    
    [Header("Other")]
    public bool isDead = true;
    public Wheel wheel;
    

    private void Update() {
        if(isDead) return;
        
        visualV.localRotation = Quaternion.Lerp(visualV.localRotation,Quaternion.Euler(0, 0, tiltV),Time.deltaTime * 3);
        visualU.localRotation = Quaternion.Lerp(visualU.localRotation,Quaternion.Euler(tiltU, 0, 0),Time.deltaTime * 3);

        
        
        float y = (Input.GetKeyDown(KeyCode.W) ? tiltingForce : 0) -
                  (Input.GetKeyDown(KeyCode.S) ? tiltingForce : 0);
        float x = (Input.GetKeyDown(KeyCode.D) ? tiltingForce : 0) -
                  (Input.GetKeyDown(KeyCode.A) ? tiltingForce : 0);
        tiltU -= y;
        tiltV += x;
        rb.AddForce(Vector3.forward * movementForce * y, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void FixedUpdate() {
        if ((Mathf.Abs(tiltU) > 85 || Mathf.Abs(tiltV) > 85) && !isDead) {
            // lost
            rb.constraints = RigidbodyConstraints.None;
            isDead = true;
            GameManager.instance.Die();
            return;
        }

        
        tiltU += tiltU * Mathf.Clamp(Mathf.Abs(gravityTiltEffect / (900 * naturalGravity)), -maxTiltSpeed * naturalGravity * 0.85f,maxTiltSpeed * naturalGravity * 0.85f);
        tiltV += tiltV * Mathf.Clamp(Mathf.Abs(gravityTiltEffect / (900 * naturalGravity)), -maxTiltSpeed * naturalGravity * 0.85f,maxTiltSpeed * naturalGravity * 0.85f);
    }

    private void OnCollisionStay(Collision collisionInfo) {
        wheel.velocity = collisionInfo.relativeVelocity;
    }
}
