using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public CanvasGroup pauseMenu, optionsMenu;

    public Transform optionsPos, resumePos, exitPos, selectorPos;

    int selection = 0;

    bool select;

    int currentMenu, none, main, options;

	// Use this for initialization
	void Start () {
        none = 0;
        main = 1;
        options = 2;

        currentMenu = none;
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 0) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) selection--;
            if (Input.GetKeyDown(KeyCode.DownArrow)) selection++;

            if (currentMenu == main) {
                Main();
                pauseMenu.alpha = 1;
                optionsMenu.alpha = 0;
            }
            else if (currentMenu == options) {
                OptionsMenu();
                pauseMenu.alpha = 0;
                optionsMenu.alpha = 1;
            }
            else currentMenu = main;
        }
        else {
            pauseMenu.alpha = 0;
            optionsMenu.alpha = 0;
        }

        //only one tick of selection
        if (select) {
            select = false;
            selection = 0;
        }
    }

    void Main() {
        currentMenu = main;
        selectorPos = transform.Find("Main").transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) select = true;
        else select = false;

        if (selection > 2) selection = 0;
        if (selection < 0) selection = 2;

        switch (selection) {
            case 0:
                selectorPos.position = optionsPos.position;
                if (select) currentMenu = options;
                break;
            case 1:
                selectorPos.position = resumePos.position;
                if (select) Time.timeScale = 1;
                break;
            case 2:
                selectorPos.position = exitPos.position;
                break;
            default:
                Debug.LogError("Something went wrong selecting menu objects.");
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) currentMenu = none;
    }

    void OptionsMenu() {
        currentMenu = options;

        selectorPos = transform.Find("Options").transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) select = true;
        else select = false;

        if (selection > 6) selection = 0;
        if (selection < 0) selection = 6;

        switch (selection) {
            case 0:
                selectorPos.position = optionsPos.position;
                if (select) currentMenu = main;
                break;
            case 1:
                selectorPos.position = resumePos.position;
                if (select) ;
                break;
            case 2:
                selectorPos.position = exitPos.position;
                break;
            case 3:
                selectorPos.position = exitPos.position;
                break;
            case 4:
                selectorPos.position = exitPos.position;
                break;
            case 5:
                selectorPos.position = exitPos.position;
                break;
            case 6:
                selectorPos.position = exitPos.position;
                break;
            default:
                Debug.LogError("Something went wrong selecting menu objects.");
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) currentMenu = none;
    }
}
