using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public Transform emergeTransform;
    Player player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();		
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            player.TeleportTo(emergeTransform);
        }
    }
}
