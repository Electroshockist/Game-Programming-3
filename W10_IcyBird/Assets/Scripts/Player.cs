using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity {

    Vector2 jumpForce;
    int force = 300;

    public float powerupTime;
    float reaminingPowerUpTime;

    public Sprite Default, PoweredUp;
    SpriteRenderer spriteRenderer;

    public AudioSource[] audioSource;
    public AudioClip sfxDie, sfxJump, sfxCollect, sfxBreak;

    public static bool dead;

    bool poweredUp;
    

	// Use this for initialization
	void Start () {
        dead = false;

        // Set the 'jumpForce' of the Player to move them up
        jumpForce = new Vector2(0, force);

        audioSource = Camera.main.gameObject.GetComponents<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Check if Obstacle has a Rigidbody
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            // No rigidbody added, so add one
            rb = gameObject.AddComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            Debug.Log(reaminingPowerUpTime);
            if (poweredUp) {
                reaminingPowerUpTime -= Time.deltaTime;
                spriteRenderer.sprite = PoweredUp;
                if (reaminingPowerUpTime <= 0) poweredUp = false;
            }
            else spriteRenderer.sprite = Default;

            // Check if 'Jump' button was pressed
            if (Input.GetButtonDown("Jump")) {
                PlaySound(sfxJump);
                // Reset velocity so gravity does not have an affect on the AddForce() below
                rb.velocity = Vector2.zero;

                // Add a force to move the player up
                rb.AddForce(jumpForce);
            }
        }
        else if (rb.velocity.y > 0) rb.velocity = new Vector2(0, 0);
	}

    // Checks if Player hits anything tagged as a Trigger
    // - Not really concerned on the type of gameObject it hits at this point
    void OnTriggerEnter2D(Collider2D c) {
        // Call Die() to kill player, store score and end level
        if (!dead) {
            if (c.tag == "Obstacle") CollideWithObstacle(c.gameObject);
            if (c.tag == "Collectable") PowerUp(c.gameObject);
        }
    }

    void Die() {
        dead = true;
        // Check of the player got a highscore
        if (GameManager.instance.score >= PlayerPrefs.GetInt("Highscore"))
            // Update 'highscore' if previous score was beat
            PlayerPrefs.SetInt("Highscore", GameManager.instance.score);

        // Go back to the main scene
        // - Can also go to GameOver screen
        PlaySound(sfxDie);

        Invoke("LoadTitle", 1.0f);

    }

    void CollideWithObstacle(GameObject c) {
        if (poweredUp) {
            PlaySFX(sfxBreak);
            Destroy(c);
        }
        else Die();
    }

    void PowerUp(GameObject c) {
        poweredUp = true;
        reaminingPowerUpTime = powerupTime;
        PlaySFX(sfxCollect);
        Destroy(c);
    }

    void LoadTitle() {SceneManager.LoadScene("TitleScene");}

    void PlaySound(AudioClip clip, float Volume = 1.0f) {
        audioSource[0].clip = clip;
        audioSource[0].volume = Volume;
        audioSource[0].Play();
    }
    void PlaySFX(AudioClip clip, float Volume = 1.0f) {
        audioSource[1].clip = clip;
        audioSource[1].volume = Volume;
        audioSource[1].Play();
    }

}



