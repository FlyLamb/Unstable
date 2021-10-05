using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
    public Rigidbody rb;
    public float radius;

    public Transform axis;

    private void FixedUpdate() {
        transform.Rotate(axis.right * (rb.velocity.magnitude * Mathf.Sign(rb.velocity.z) / (2*Mathf.PI * radius)), Space.World);
    }
}