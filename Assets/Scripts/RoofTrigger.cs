using UnityEngine;

public class RoofTrigger : MonoBehaviour {
    private bool generated = false;
    private void OnCollisionEnter(Collision collision) {
        if (generated) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        generated = true;
        FindObjectOfType<RouteGenerator>().Generate();
        FindObjectOfType<BackgroundGenerator>().GenerateBackground(3);
    }
}
