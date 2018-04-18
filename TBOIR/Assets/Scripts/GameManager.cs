using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool paused;
    	
	// Update is called once per frame
	void Update () {
        //Pause Toggle
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
        }
    }
}
