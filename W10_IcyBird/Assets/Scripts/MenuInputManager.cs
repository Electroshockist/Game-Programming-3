using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager : MonoBehaviour
{
    // UI Components must be dragged into the variables
    public Button startButton;
    public Button quitButton;

    public Text highScore;

    // Use this for initialization
    void Start()
    {
        // Links the functions from GameManager when the game starts and restarts
        startButton.onClick.AddListener(GameManager.instance.StartGame);
        quitButton.onClick.AddListener(GameManager.instance.QuitGame);

        // Check if a highscore exists
        if(PlayerPrefs.GetInt("Highscore") > 0)
            // Display highscore on title screen
            highScore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        
    }
}
