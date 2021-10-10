using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RouteGenerator : MonoBehaviour {
    public GameObject[] roofs;
    private int progress;
    public Vector4 genRange;
    private Vector3 position;
    public int genOnStart = 5;
    public GameObject plank;

    private List<GameObject> instances;
    
    private void Start() {
        instances = new List<GameObject>();
        FindObjectOfType<BackgroundGenerator>().GenerateBackground(6);
        for (int i = 0; i < genOnStart; i++) {
            Generate(false);
            
        }
    }

    public void Generate(bool destroyLast = true) {
        var roof = roofs[Random.Range(0, roofs.Length)];
        var rf = Instantiate(roof, position, roof.transform.rotation, transform);
        rf.GetComponent<BuildingGenerator>().GenerateRooftop();
        position += new Vector3(0, Random.Range(genRange.z, genRange.w), Random.Range(genRange.x, genRange.y));
        instances.Add(rf);
        progress++;
        
        if(progress < 2) return;
        
        var a = instances[progress-2].transform.Find("Corner A").position;
        var b = rf.transform.Find("Corner B").position;

        var mid = Vector3.Lerp(a, b, 0.5f);
        var go = Instantiate(plank, a, Quaternion.identity, rf.transform);
        go.GetComponent<Plank>().Connect(a, b);

        if (destroyLast && progress > 7) {
            var w = instances[progress-7];
            Destroy(w);
        }
    }
}