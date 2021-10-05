using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        GameManager.instance.Score(10);
        GetComponent<AudioSource>().Play();
        Destroy(GetComponent<BoxCollider>());
        
        print("Point!");
    }
}
