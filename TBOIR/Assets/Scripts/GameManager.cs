using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static string currentscene;
    public const string mainmenu = "main menu";
    public const string level = "level";

    private static bool created = false;

    public static bool dead = false;

    void Awake() {
        currentscene = mainmenu;
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    // Update is called once per frame
    void Update () {
        //checks scene
        if (currentscene == mainmenu) {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) {
                currentscene = level;
                SceneManager.LoadScene(level, LoadSceneMode.Single);
            }
            //Quit(TODO)
            if (Input.GetKeyDown(KeyCode.Escape) && currentscene == mainmenu) Debug.Log("Quit");
        }
        //Pause
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0) {
            Time.timeScale = 0;
        }
    }
}
