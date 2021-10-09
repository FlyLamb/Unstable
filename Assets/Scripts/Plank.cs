using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour {

    public float width, height;

    private void Start() {
        //Connect(Vector3.zero, Vector3.forward * 10 + Vector3.up * 4); 
    }

    public void Connect(Vector3 a, Vector3 b) {
        Mesh mesh = new Mesh();

        a = transform.InverseTransformPoint(a);
        b = transform.InverseTransformPoint(b);
        
        Vector3[] vertices = {
            new(a.x - width, a.y - height, a.z),
            new(a.x + width, a.y - height, a.z),
            new(a.x + width, a.y + height, a.z),
            new(a.x - width, a.y + height, a.z),
            new(b.x - width, b.y + height, b.z),
            new(b.x + width, b.y + height, b.z),
            new(b.x + width, b.y - height, b.z),
            new(b.x - width, b.y - height, b.z),
        };

        mesh.vertices = vertices;

        mesh.triangles = new int[] {
            0, 2, 1, //face front
            0, 3, 2,
            2, 3, 4, //face top
            2, 4, 5,
            1, 2, 5, //face right
            1, 5, 6,
            0, 7, 4, //face left
            0, 4, 3,
            5, 4, 7, //face back
            5, 7, 6,
            0, 6, 7, //face bottom
            0, 1, 6
        };
        mesh.Optimize();
        
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        
    }
}
