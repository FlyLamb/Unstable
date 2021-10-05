using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour {
    public Transform relativeTo;
    

    private void Start() {
    }

    private void Update() {
        transform.position = relativeTo.position;
    }
}
