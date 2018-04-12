using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public SpriteRenderer entityRenderer;
    // Use this for initialization
    void Start () {
        
        if (baseSpeed <= 0) baseSpeed = 2.0f;

        if (speed <= 0) baseSpeed = 2.0f;

        if (baseJumpForce <= 0) baseJumpForce = 320.0f;
    }
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();

        anim.KoopaAnimate();

        anim.walking = true;

        Body.velocity = new Vector2(speed, Body.velocity.y);

        Flip();
    }
}
