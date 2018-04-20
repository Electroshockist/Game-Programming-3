using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    bool paused;
    	
	// Update is called once per frame
	void Update () {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0) {
            Time.timeScale = 0;
            paused = true;
        }
        if(!paused) Time.timeScale = 1;
    }
}
