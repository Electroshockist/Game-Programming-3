using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public bool isAttached;
    void Attached() {
        Transform player = GameObject.Find("Player").transform;
        this.transform.position = new Vector2(player.position.x, player.transform.position.y + 35);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttached) Attached();		
	}
}
