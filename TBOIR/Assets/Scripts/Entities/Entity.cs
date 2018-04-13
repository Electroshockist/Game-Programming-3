using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414 // private field assigned but not used.


public class Entity : MonoBehaviour {
    public Rigidbody2D Body;
    public AnimationHandler anim;
    public float speed;

    MusicScript sound;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<AnimationHandler>();
        sound = GameObject.Find("Main Camera").GetComponent<MusicScript>();
        if(speed <= 0.0f) speed = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//basis for powerup
public class Effect {
    public int ID;
    public float remaingDuration;
    public bool active;

    public Effect(int _ID, float _remainingDuration) { }

    void Deactivate() {
        active = false;
    }

    public void Update() {
        if (remaingDuration > 0) remaingDuration -= Time.deltaTime;
        else Deactivate();
    }
}

