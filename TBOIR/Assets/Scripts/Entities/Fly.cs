using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Entity {
    Transform playerTransform;
    public AudioSource FlySound;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (dead()) {
            this.GetComponent<Animator>().SetBool("Dead", true);
            //immediately destroys colliders
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.gameObject, 0.20f);

            //can't move while dying
            speed = 0;
        }
        Vector3 direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        direction.Normalize();

        Body.velocity = new Vector2(speed * direction.x, speed * direction.y);
	}
}
