using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unicycle : MonoBehaviour {
    [SerializeField] private Transform visualV, visualU;
    public float tiltU;
    public float tiltV;
    public float tiltEffect = 2;
    public float naturalGravity = 1;
    public float forceMultiplier = 2;
    public float maxFVelocity = 1;
    public float tiltClamp = 0.04f;
    public float maxSpeed = 5;

    public bool isDead = true;

    [SerializeField] private Rigidbody rb;

    private void Update() {
        if(isDead) return;
        
        visualV.localRotation = Quaternion.Lerp(visualV.localRotation,Quaternion.Euler(0, 0, tiltV),Time.deltaTime * 3);
        visualU.localRotation = Quaternion.Lerp(visualU.localRotation,Quaternion.Euler(tiltU, 0, 0),Time.deltaTime * 3);

        
        
        float y = (Input.GetKeyDown(KeyCode.W) ? forceMultiplier : 0) -
                  (Input.GetKeyDown(KeyCode.S) ? forceMultiplier : 0);
        float x = (Input.GetKeyDown(KeyCode.D) ? forceMultiplier : 0) -
                  (Input.GetKeyDown(KeyCode.A) ? forceMultiplier : 0);
        tiltU -= y;
        tiltV += x;
        rb.AddForce(Vector3.forward * maxFVelocity * y, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void FixedUpdate() {
        if ((Mathf.Abs(tiltU) > 80 || Mathf.Abs(tiltV) > 80) && !isDead) {
            // lost
            rb.constraints = RigidbodyConstraints.None;
            isDead = true;
            GameManager.instance.Die();
            return;
        }

        
        tiltU += tiltU * Mathf.Clamp(Mathf.Abs(tiltEffect / (900 * naturalGravity)), -tiltClamp * naturalGravity * 0.85f,tiltClamp * naturalGravity * 0.85f);
        tiltV += tiltV * Mathf.Clamp(Mathf.Abs(tiltEffect / (900 * naturalGravity)), -tiltClamp * naturalGravity * 0.85f,tiltClamp * naturalGravity * 0.85f);
    }
}
