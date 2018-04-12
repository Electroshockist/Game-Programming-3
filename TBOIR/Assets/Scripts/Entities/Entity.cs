using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
