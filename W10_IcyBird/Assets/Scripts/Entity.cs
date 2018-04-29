using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Vector2 velocity;
    public float speed = -4;
    public float range = 4;
    public Rigidbody2D rb;

     void Start()
    {
        if (!rb)
            // No rigidbody added, so add one
            rb = gameObject.AddComponent<Rigidbody2D>();

        // Position Obstacle randomly along 'y' axis
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - range * Random.value,
            transform.position.z
        );
    }

}

  
