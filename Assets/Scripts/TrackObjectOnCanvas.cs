using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjectOnCanvas : MonoBehaviour {
    public Transform toTrack;
    public Vector3 offset3d;
    public Vector2 offset2d;
    
    private void Update() {
        if (toTrack == null) toTrack = GameObject.FindObjectOfType<Unicycle>().transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        transform.position = Camera.main.WorldToScreenPoint(toTrack.position + offset3d) + (Vector3)offset2d;
    }
}
