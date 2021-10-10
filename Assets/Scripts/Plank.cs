using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plank : MonoBehaviour {

    public float width, height;

    public GameObject coin;

    public void Connect(Vector3 a, Vector3 b) {
        Mesh mesh = new Mesh();
        GenerateCoins(Random.Range(-1,10), a,b);
        a = transform.InverseTransformPoint(a);
        b = transform.InverseTransformPoint(b);
        
        Vector3[] vertices = {
            new Vector3(a.x - width, a.y - height, a.z),
            new Vector3(a.x + width, a.y - height, a.z),
            new Vector3(a.x + width, a.y + height, a.z),
            new Vector3(a.x - width, a.y + height, a.z),
            new Vector3(b.x - width, b.y + height, b.z),
            new Vector3(b.x + width, b.y + height, b.z),
            new Vector3(b.x + width, b.y - height, b.z),
            new Vector3(b.x - width, b.y - height, b.z),
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

    public void GenerateCoins(int amount, Vector3 a, Vector3 b) {
        if(amount <= 0) return;
        for (int i = 0; i < amount; i++) {
            var p = Vector3.Lerp(a, b, (float)i / amount);
            Instantiate(coin,  p+ Vector3.up * 0.4f + Coinoise(p), Quaternion.identity, transform);
        }
    }

    private Vector3 Coinoise(Vector3 b) {
        var scale = 0.10214124f;
        var magnitude = .3f;

        float x = Mathf.PerlinNoise(b.z * scale, 0);
        x -= 0.5f;
        x *= 10;
        x = Mathf.Clamp(x, -magnitude, magnitude);
        
        return new Vector3(x, 0, 0);
    }
}
