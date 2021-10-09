using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class PointTrigger : MonoBehaviour {
    public SkinnedMeshRenderer flag;
    private void OnTriggerEnter(Collider other) {
        GameManager.instance.Score(10);
        GetComponent<AudioSource>().Play();
        Destroy(GetComponent<BoxCollider>());
        flag.TweenValueFloat(100, 0.2f, (w) => flag.SetBlendShapeWeight(0, w) ).SetFrom(0);
        print("Point!");
    }
}
