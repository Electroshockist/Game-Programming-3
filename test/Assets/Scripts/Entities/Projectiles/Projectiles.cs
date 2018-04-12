using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {

    public AnimationHandler anim;

    // Use this for initialization
    void Start () {
        //component getters
        anim = GetComponent<AnimationHandler>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
