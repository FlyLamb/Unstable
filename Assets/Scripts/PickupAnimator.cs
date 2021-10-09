using System;
using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;

public class PickupAnimator : MonoBehaviour {
    public float bobbingSpeed = 1f;
    public float bobbingRadius = 0.2f;
    public float spinSpeed = 30;

    private bool pickedUp = false;
    private void Start() {
        transform.TweenLocalPositionY(bobbingRadius, bobbingSpeed).SetFrom(0).SetPingPong().SetEase(EaseType.SineInOut).SetInfinite();
    }

    private void Update() {
        transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
        if(pickedUp)
            transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
    }

    public void Pickup() {
        pickedUp = true;
        transform.TweenCancelAll();
        transform.TweenLocalPositionY(2, 1);
        transform.TweenLocalScale(Vector3.zero, 1).SetOnComplete(() => {
            Destroy(gameObject);
        });
    }
}