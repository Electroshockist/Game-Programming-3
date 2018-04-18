using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public CanvasGroup pauseMenu;

    public Transform options, resume, exit, selector;

    int selection = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.paused) {
            pauseMenu.alpha = 1;

            if (Input.GetKeyDown(KeyCode.UpArrow)) selection--;
            if (Input.GetKeyDown(KeyCode.DownArrow)) selection++;

            if (selection > 2) selection = 0;
            if (selection < 0) selection = 2;

            switch (selection) {
                case 0:
                    selector.position = options.position;
                    break;
                case 1:
                    selector.position = resume.position;
                    break;
                case 2:
                    selector.position = exit.position;
                    break;
                default: Debug.LogError("Something went wrong selecting menu objects.");
                    break;
            }
        }
        else pauseMenu.alpha = 0;
	}
}
