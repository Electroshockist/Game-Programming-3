using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414 // private field assigned but not used.


public class Entity : MonoBehaviour {
    public Rigidbody2D Body;

    [HideInInspector]
    public AnimationHandler anim;
    public float baseSpeed, speed, health;
    public bool canTakeDamage, isPlayer, playedHurtSound, playedDeadSound;


    // Use this for initialization
    void Awake () {
        anim = GetComponent<AnimationHandler>();
        canTakeDamage = true;
        if (baseSpeed <= 0.0f) baseSpeed = 100.0f;
        if (speed <= 0.0f) speed = baseSpeed;
        if (health <= 0) health = 6;
	}

    void setDamageable() {
        canTakeDamage = true;
    }

    public bool dead() {
        if (health <= 0) return true;
        else return false;        
    }

    public void Damage(float damage) {
        if (canTakeDamage) {
            health -= damage;

            if (isPlayer) {
                canTakeDamage = false;
                Invoke("setDamageable", 1.0f);
            }
        }
    }
}

//basis for powerup(unused atm)
public class Effect {
    public int ID;
    public float remaingDuration;
    public bool active;

    public Effect(int _ID) {
        ID = _ID;
    }

    void Deactivate() {
        active = false;
    }

    public void Update() {
        if (remaingDuration > 0) remaingDuration -= Time.deltaTime;
        else Deactivate();
    }
}

