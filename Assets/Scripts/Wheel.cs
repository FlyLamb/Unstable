using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
    public float radius;

    public Transform axis;
    public Vector3 velocity;
    
    private void FixedUpdate() {
        transform.Rotate(axis.right * (velocity.magnitude * Mathf.Sign(-velocity.z) / (2*Mathf.PI * radius)), Space.World);
    }
}