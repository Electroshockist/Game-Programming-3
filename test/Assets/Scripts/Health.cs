using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;
    public RectTransform healthbar;

	// Use this for initialization
	void Start () {
		
	}


    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Projectile_Enemy"){
            health--;
        }
    }
}
