using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public CanvasGroup pauseMenu, optionsMenu, deathMenu;

    public Transform optionsPos, resumePos, exitPos, selectorPos, sfxPos, MusicPos, MapOpacityPos, FullscreenPos, FilterPos;

    public Sprite[] valueTicks1, valueTicks2;

    public Image sfxVal, musicVal, mapOpacityVal;
    
    int selection = 0;

    //volume control
    int sfxVol = 10;
    int musicVol = 10;
    int mapOpacity = 10;

    bool select;

    int currentMenu, none, main, options, dead;

	// Use this for initialization
	void Start () {
        none = 0;
        main = 1;
        options = 2;
        dead = 3;

        currentMenu = none;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(Time.timeScale);
        //set music volume
        MusicScript.music.volume = (float)musicVol / 10;
        if (GameManager.dead) {
            currentMenu = dead;
            DeathMenu();
        }
        else {

            //checks if music is muted
            if (MusicScript.music.mute) musicVal.sprite = valueTicks1[0];
            //set textures
            else musicVal.sprite = valueTicks1[musicVol];
            sfxVal.sprite = valueTicks1[sfxVol];
            mapOpacityVal.sprite = valueTicks1[mapOpacity];

            if (Time.timeScale == 0) {
                //for selecting values
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) select = true;
                else select = false;
                //moves seletor
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
                //disables all menus
                pauseMenu.alpha = 0;
                optionsMenu.alpha = 0;
            }

            //only one tick of selection
            if (select) select = false;
        }
    }

    void Main() {
        selectorPos = transform.Find("Main").transform.GetChild(0);

        //enable pause menu, disable options menu
        pauseMenu.alpha = 1;
        optionsMenu.alpha = 0;

        //reset selector position if out of bounds
        if (selection > 2) selection = 0;
        if (selection < 0) selection = 2;

        switch (selection) {
            case 0:
                selectorPos.position = optionsPos.position;
                //open options menu
                if (select) {
                    currentMenu = options;
                    selection = 0;
                }
                break;
            case 1:
                selectorPos.position = resumePos.position;
                //unpause game
                if (select) Unpause();
                break;
            case 2:
                //does nothing right now
                selectorPos.position = exitPos.position;
                if (select) {
                    Time.timeScale = 1;
                    GameManager.currentscene = GameManager.mainmenu;
                    SceneManager.LoadScene(GameManager.currentscene, LoadSceneMode.Single);

                }
                break;
            default:
                Debug.LogError("Something went wrong selecting pause menu objects.");
                break;
        }
        //unpause game
        if (Input.GetKeyDown(KeyCode.Escape)) Unpause();
    }

    void OptionsMenu() {
        //enable options menu, disable pause menu
        pauseMenu.alpha = 0;
        optionsMenu.alpha = 1;

        selectorPos = transform.Find("Options").transform.GetChild(0);

        //reset selector position if out of bounds
        if (selection > 4) selection = 0;
        if (selection < 0) selection = 4;
        
        switch (selection) {
            case 0:
                selectorPos.position = sfxPos.position;
                //add/subtract sfx volume(not implemented)
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) sfxVol = AddOne(sfxVol,10);
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) sfxVol = SubtractOne(sfxVol);
                break;
            case 1:
                selectorPos.position = MusicPos.position;
                //add/subtract music volume
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) musicVol = AddOne(musicVol, 10);

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) musicVol = SubtractOne(musicVol);
                    break;
            case 2:
                selectorPos.position = MapOpacityPos.position;
                //add/subtract map opacity(not implemented)
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) mapOpacity = AddOne(mapOpacity, 10);
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) mapOpacity = SubtractOne(mapOpacity);
                break;
            case 3:
                //does nothing right now
                selectorPos.position = FullscreenPos.position;
                break;
            case 4:
                //does nothing right now
                selectorPos.position = FilterPos.position;
                break;
            default:
                Debug.LogError("Something went wrong selecting option menu objects.");
                break;
        }
        //return to pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentMenu = main;
            selection = 0;
        }
    }

    void DeathMenu() {
        //enable options menu, disable pause menu
        pauseMenu.alpha = 0;
        optionsMenu.alpha = 0;
        deathMenu.alpha = 1;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            deathMenu.alpha = 0;
            Time.timeScale = 1;
            currentMenu = none;
            GameManager.dead = false;
            GameManager.currentscene = GameManager.mainmenu;
            SceneManager.LoadScene(GameManager.currentscene, LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            deathMenu.alpha = 0;
            Time.timeScale = 1;
            currentMenu = none;
            GameManager.dead = false;
            GameManager.currentscene = GameManager.level;
            SceneManager.LoadScene(GameManager.currentscene, LoadSceneMode.Single);
        }

    }

    void Unpause() {
        currentMenu = none;
        Time.timeScale = 1;
    }
    int AddOne(int value, int maxvalue) {
        //valuse cannot go above 10 or under 0
        if (value >= maxvalue) return value;
        else value++;
        return value;
    }
    int SubtractOne(int value) {
        if (value <= 0) return value;
        else value--;
        return value;
    }
}
