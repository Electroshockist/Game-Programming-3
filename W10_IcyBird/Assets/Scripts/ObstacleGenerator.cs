using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour {

    // Keep a reference to prefab to create
    public GameObject obstacle;
    public GameObject obstacleWithShard;

    int random;


    // Used to hold a reference to the text UI part of the HUD
    Text scoreText;

	// Use this for initialization
	void Start () {

        // Set the Class variable score to 0 when 'Level1' starts
        GameManager.instance.score = 0;

        // Find and keep a reference to the Text on screen
        scoreText = GameObject.Find("Text_Score").GetComponent<Text>();

        // Update the text with the starting score
        scoreText.text = GameManager.instance.score.ToString();

        // Calls 'CreateObstacle' after 1s and then repeats every 1.5s after
        InvokeRepeating("CreateObstacle", 1.0f, 1.5f);
	}

    void CreateObstacle() {
        random = Random.Range(0, 4);
        // Create the GameObject at the position of the ObstacleGenerator gameObject in the Scenes
        if (random < 3) Instantiate(obstacle, transform.position, Quaternion.identity);

        else Instantiate(obstacleWithShard, transform.position, Quaternion.identity);


        // Update the text with the score
        scoreText.text = (++GameManager.instance.score).ToString(); ;
    }
}

