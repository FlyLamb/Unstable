using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundGenerator : MonoBehaviour {
    public GameObject[] roofs;
    private Vector3 current;
    private Queue<GameObject> creations;

    private void Start() {
        creations = new Queue<GameObject>();
        StartCoroutine(GenMass());
    }

    private IEnumerator GenMass() {
        for (int i = 0;; i++) {
            Generate();
            yield return new WaitForSeconds(1.5f);
            if (i > 120) {
                Destroy(creations.Dequeue());
            }
        }
    }

    private void Generate() {
        var roof = roofs[Random.Range(0, roofs.Length)];
        current = new Vector3(Random.Range(-5f, -40f), Random.Range(8f, -3f), current.z + Random.Range(4f, 15f));
        var go = Instantiate(roof, current, roof.transform.rotation, transform);
        go.GetComponent<BuildingGenerator>().height = 12;
        creations.Enqueue(go);
    }
}