using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingGenerator : MonoBehaviour {
    public Transform[] slots;
    public GameObject[] randomFloors;
    public float floorHeight = 5;
    public Gradient colors;
[Space]
    public int height = 4;
    private void Start() {
        Generate(height);
    }

    public void Generate(int c = 4) {
        GameObject f = randomFloors[Random.Range(0, randomFloors.Length)];
        Color color = colors.Evaluate(Random.Range(0f, 1f));
        for (int i = 0; i < c; i++) {
            foreach (Transform t in slots) {
                var b = Instantiate(f, t.position + Vector3.down * i * floorHeight, f.transform.rotation, transform);
                b.GetComponent<MeshRenderer>().material.color = color;
            }
            
        }
    }
}