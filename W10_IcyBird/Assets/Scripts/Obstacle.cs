using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Entity {

	// Use this for initialization
	void Start () {

        // Start velocity of Obstacle at -4 to move left
        velocity = new Vector2(speed, 0);

        // Check if Obstacle has a Rigidbody
        rb = GetComponent<Rigidbody2D>();
        if(!rb)
            // No rigidbody added, so add one
            rb = gameObject.AddComponent<Rigidbody2D>();
        // Position Obstacle randomly along 'y' axis
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - range * Random.value,
            transform.position.z);
    }

    void Update() {        
        if (Player.dead) velocity.Set(0,0);

        // Make Obstacle move to the left
        rb.velocity = velocity;

    }

    void OnTriggerEnter2D(Collider2D c) {
        // Check if Obstacle should be destroyed once off screen
        if (c.gameObject.tag == "ObstacleDestroyer")
            Destroy(gameObject);
    }
}
