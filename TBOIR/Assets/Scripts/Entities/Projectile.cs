using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity {
    public float lifetime, speedx ,speedy, damage;

	// Use this for initialization
	void Start () {
        if (lifetime <= 0) lifetime = 0.3f;
        Destroy(gameObject, lifetime);
	}

    void Update() {
        if (Time.timeScale == 0) Body.velocity = new Vector2(0, 0);
        else Body.velocity = new Vector2(speedx, speedy);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Wall") Destroy(gameObject);

        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Entity>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
