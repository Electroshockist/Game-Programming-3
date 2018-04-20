using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public CanvasGroup pauseMenu, optionsMenu;

    public Transform optionsPos, resumePos, exitPos, selectorPos, SFXPos, MusicPos, MapOpacityPos, FullscreenPos, FilterPos;

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

            //checks if current menu is main
            if (currentMenu == main) {
                Main();
            }
            //checks if current menu is options
            else if (currentMenu == options) {
                OptionsMenu();
            }
            //sets main to default menu
            else currentMenu = main;
        }
        else {
            pauseMenu.alpha = 0;
            optionsMenu.alpha = 0;
        }

        //only one tick of selection
        if (select) {
            select = false;
        }
    }

    void Main() {
        currentMenu = main;
        selectorPos = transform.Find("Main").transform.GetChild(0);

        pauseMenu.alpha = 1;
        optionsMenu.alpha = 0;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) select = true;
        else select = false;

        if (selection > 2) selection = 0;
        if (selection < 0) selection = 2;

        switch (selection) {
            case 0:
                selectorPos.position = optionsPos.position;
                if (select) {
                    currentMenu = options;
                    selection = 0;
                }
                break;
            case 1:
                selectorPos.position = resumePos.position;
                if (select) Time.timeScale = 1;
                break;
            case 2:
                selectorPos.position = exitPos.position;
                break;
            default:
                Debug.LogError("Something went wrong selecting pause menu objects.");
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentMenu = none;
            Time.timeScale = 1;
        }
    }

    void OptionsMenu() {
        currentMenu = options;

        pauseMenu.alpha = 0;
        optionsMenu.alpha = 1;

        selectorPos = transform.Find("Options").transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) select = true;
        else select = false;

        if (selection > 4) selection = 0;
        if (selection < 0) selection = 4;

        switch (selection) {
            case 0:
                selectorPos.position = SFXPos.position;
                break;
            case 1:
                selectorPos.position = MusicPos.position;
                break;
            case 2:
                selectorPos.position = MapOpacityPos.position;
                break;
            case 3:
                selectorPos.position = FullscreenPos.position;
                break;
            case 4:
                selectorPos.position = FilterPos.position;
                break;
            default:
                Debug.LogError("Something went wrong selecting option menu objects.");
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentMenu = main;
            selection = 0;
        }
    }
}
