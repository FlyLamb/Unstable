using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public PickupAnimator animator;
    public AudioSource source;
    
    public int pointValue;
    
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        OnPickup();
    }

    protected virtual void OnPickup() {
        GameManager.instance.Score(pointValue);
        animator.Pickup();
        source.Play();
        Destroy(GetComponent<Collider>());
        Destroy(gameObject, 4);
    }
}