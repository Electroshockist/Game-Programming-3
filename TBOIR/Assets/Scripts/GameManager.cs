using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0) {
            Time.timeScale = 0;
        }
    }
}
