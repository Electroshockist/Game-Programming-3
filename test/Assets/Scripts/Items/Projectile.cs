using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifeTime;
	// Use this for initialization
	void Start () {
        if (lifeTime == 0.0f) {
            lifeTime = 2.0f;
        }
	}
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Trigger Detected: " + collision);
        if (collision.gameObject.tag == "Collectible") {
            Destroy(collision.gameObject);
        }
    }
}
