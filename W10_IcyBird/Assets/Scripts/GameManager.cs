using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Not needed with get;set; methods below
    //static GameManager _instance = null;
    //int _score;

	// Use this for initialization
	void Start () {

        // Start score at 0
        score = 0;

        // Check if instance exists
        if (!instance)
        {
            // Object does not exist, keep a reference to it
            instance = this;
            // Don't destroy object on Scene switch
            DontDestroyOnLoad(gameObject);
        }
        else
            // If gameObject exists, destroy second copy of gameObject
            DestroyImmediate(gameObject);
	}
	
	public void StartGame()
    {
        // Switch to 'Level1' Scene
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        // Quit game when game is built
        Application.Quit();
    }

    public int score
    {
        get; set;
    }

    public static GameManager instance
    {
        get; set;
    }
}
