using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField]
    int _points;
	// Use this for initialization
	void Start () {
        if(points <=0) {
            points = 10;
            Debug.Log("Points not set. Defaulting to " + points);
        }
    }

    public int points {
            get;set;
    }
}
