using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Entity {
    Transform playerTransform;
    AudioSource FlySound;

    public AudioClip[] deadSound;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("Player").transform;
        FlySound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (dead()) {
            if (!playedDeadSound) {
                FlySound.PlayOneShot(deadSound[(int)Mathf.Round(Random.Range(0.0f, deadSound.Length - 1))]);
                playedDeadSound = true;
            }
            this.GetComponent<Animator>().SetBool("Dead", true);
            //immediately destroys colliders
            Destroy(this.GetComponent<CircleCollider2D>());
            Destroy(this.gameObject, 0.45f);

            //can't move while dying
            speed = 0;
        }
        Vector3 direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        direction.Normalize();

        Body.velocity = new Vector2(speed * direction.x, speed * direction.y);
	}
}
