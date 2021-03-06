﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414 // private field assigned but not used.

public class Player : Entity {
    Vector2 moveValue = new Vector2();
    Transform currentObject, topObject, bottomObject, leftObject, rightObject;
    public Transform camera1, camera2, mainCamera;
    
    Effect Invincibility = new Effect(0);

    public AudioClip[] tearSound, hurtSound, deadSound;
    AudioSource sound;

    int room = 0;

    void Swapcams() {
        if (room % 2 == 0) mainCamera.position = camera2.position;
        else mainCamera.position = camera1.position;
    }

    public void TeleportTo(Transform emergeTransform) {
        Swapcams();
        this.transform.position = emergeTransform.position;
        room++;
    }

    //--------------------------Surrogate Variables--------------------------//
    // for animationHandler(only used to clean code)

    bool aquireItem, isWalkingHorizontal;

    void SetSurrogates() {
        anim.type.crying = crying;

        projectilePrefab.speed = projectileSpeed;

        anim.type.aquireItem = aquireItem;

        anim.type.hurt = playedHurtSound;

        anim.type.dead = dead();
    }

    //------------------------------Tear Stuff------------------------------//
    Projectile projectile;

    public float projectileSpeed, shotSpeed;
    public Projectile projectilePrefab;
    Transform projectileSpawn, currentEye;

    int currentEyeInt = 0;

    bool crying;

    //for delay between tears
    bool canCry = true;

    //tear metadata
    public float cryTime, attackSpeed, tearDelay, tears, damage, damageMultiplier;

    //runs when attacking
    void Crying() {
        if (canCry) {
            canCry = false;
            crying = true;
            ProjectileManager();
            Invoke("Waiting", cryTime);
            Invoke("SetCanCry", attackSpeed);
        }
    }
    //delay between tears
    void Waiting() {crying = false;}

    void SetCanCry() {canCry = true;}

    //deals with projectile related stuff
    void ProjectileManager() {

        //actual math from game
        tearDelay = (16 - 6 * Mathf.Sqrt(1 + 1.3f * tears)) / 60;
        attackSpeed = (30 / (tearDelay + 1))/60;

        projectilePrefab.damage = damage * damageMultiplier;

        //swaps eye
        if (currentEyeInt == 0) currentEyeInt = 1;
        else if (currentEyeInt == 1) currentEyeInt = 0;

        //projectile speed and direction math
        if (anim.type.facing.forward) {
            projectilePrefab.speedy = -projectileSpeed - projectileSpeed * (shotSpeed / 100);
            projectilePrefab.speedx = Body.velocity.x;
        }
        if (anim.type.facing.backward) {
            projectilePrefab.speedy = projectileSpeed + projectileSpeed * (shotSpeed / 100);
            projectilePrefab.speedx = Body.velocity.x;
        }
        if (anim.type.facing.right) {
            projectilePrefab.speedx = projectileSpeed + projectileSpeed * (shotSpeed / 100);
            projectilePrefab.speedy = Body.velocity.y;
        }
        if (anim.type.facing.left) {
            projectilePrefab.speedx = -projectileSpeed - projectileSpeed * (shotSpeed / 100);
            projectilePrefab.speedy = Body.velocity.y;
        }

        currentEye = currentObject.transform.GetChild(currentEyeInt);

        projectileSpawn = currentEye;
        projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        sound.PlayOneShot(tearSound[(int)Mathf.Round(Random.Range(0.0f, tearSound.Length - 1))]);
    }

    //-------------Effect Enders-------------//
    void ItemAquired() { aquireItem = false; }

    // Use this for initialization
    void Start () {
        isPlayer = true;

        sound = GetComponent<AudioSource>();

        //init head object
        anim.type.headObject = GameObject.Find("Head");
        anim.type.headSprite = anim.type.headObject.GetComponent<SpriteRenderer>();

        //init body object
        anim.type.bodyObject = GameObject.Find("Body");
        anim.type.bodySprite = anim.type.bodyObject.GetComponent<SpriteRenderer>();
        anim.type.bodyAnim = anim.type.bodyObject.GetComponent<Animator>();

        //sets objects for sides
        topObject = anim.type.headObject.transform.Find("Backward");
        bottomObject = anim.type.headObject.transform.Find("Forward");
        leftObject = anim.type.headObject.transform.Find("Left");
        rightObject = anim.type.headObject.transform.Find("Right");
        
        currentObject = bottomObject;

        //set cry time
        if (cryTime <= 0) cryTime = 7.0f / 60.0f;

        //set tears
        if (tears <= 0) tears = 1.0f;

        if (!projectilePrefab) Debug.Log("No projectilePrefab found on " + name);

        if (projectileSpeed <= 0) projectileSpeed = 160.0f;

        //damage stuff
        if (damage <= 0) damage = 3.5f;

        if (damageMultiplier <= 0) damageMultiplier = 1.0f;

        //sets player bodytype
        anim.type.player = true;
    }
	
	//Update is called once per frame
	void Update () {

        SetSurrogates();

        moveValue.Normalize();

        //adds movement to velocity
        Body.velocity = new Vector2(moveValue.x * speed, moveValue.y * speed);

        if (dead()) {
            moveValue = new Vector2(0, 0);
            GameManager.dead = true;
            if (!playedDeadSound) {
                sound.PlayOneShot(deadSound[(int)Mathf.Round(Random.Range(0.0f, deadSound.Length - 1))]);
                playedDeadSound = true;
            }
        }
        else if (!canTakeDamage) {
            if (!playedHurtSound) {
                sound.PlayOneShot(hurtSound[(int)Mathf.Round(Random.Range(0.0f, hurtSound.Length - 1))]);
                playedHurtSound = true;
            }
        }
        else playedHurtSound = false;

        if (Time.timeScale != 0 && !dead()) {

            Invincibility.Update();

            if (Invincibility.remaingDuration > 0) speed = baseSpeed + (baseSpeed / 100) * 50;
            else speed = baseSpeed;


            moveValue.x = Input.GetAxisRaw("Horizontal");
            moveValue.y = Input.GetAxisRaw("Vertical");

            //checks if you're picking up an item
            if (!aquireItem) {

                //flips body if moving left
                if (moveValue.x > 0) anim.type.moving.ForceFacing("right");
                else if (moveValue.x < 0) anim.type.moving.ForceFacing("left");

                //vertical movement animations
                if (moveValue.y < 0) anim.type.moving.ForceFacing("forward");
                else if (moveValue.y > 0) anim.type.moving.ForceFacing("backward");

                //if not moving
                if (moveValue.x == 0 && moveValue.y == 0) anim.type.moving.ForceFacing("none");


                //facing animations
                if (Input.GetKey(KeyCode.UpArrow)) {
                    anim.type.facing.ForceFacing("backward");
                    currentObject = topObject;
                    Crying();
                }
                //resets backward animation
                else anim.type.facing.backward = false;

                if (Input.GetKey(KeyCode.DownArrow)) {
                    anim.type.facing.ForceFacing("forward");
                    currentObject = bottomObject;
                    Crying();
                }
                //resets forward animation
                else anim.type.facing.forward = false;

                if (Input.GetKey(KeyCode.LeftArrow)) {
                    anim.type.facing.ForceFacing("left");
                    currentObject = leftObject;
                    Crying();
                }
                //resets left animation
                else anim.type.facing.left = false;

                if (Input.GetKey(KeyCode.RightArrow)) {
                    anim.type.facing.ForceFacing("right");
                    currentObject = rightObject;
                    Crying();
                }
                //resets forward animation
                else anim.type.facing.right = false;
            }

            //item aquired animation
            if (Input.GetKeyDown(KeyCode.Space)) {
                aquireItem = true;
                Invoke("ItemAquired", 3.0f);
            }

            //Powerup Activation
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                Invincibility.remaingDuration = 7.0f;
                anim.type.invincible = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") Damage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Collectable") {
            health++;
            collision.gameObject.GetComponent<Collectible>().isAttached = true;
            aquireItem = true;
            Invoke("ItemAquired", 3.0f);
            Destroy(collision.gameObject, 3.0f);
        }
    }
    

    //public void SaveGame() {
    //    LoadSaveManager.GameStateData.DataPlayer PlayerData = GameManager.StateManager.GameState.Player;

    //    PlayerData.Health = health;
    //    GameManager.Instance.SaveGame();
    //}
    //public void LoadGame() {
    //    LoadSaveManager.GameStateData.DataPlayer PlayerData = GameManager.StateManager.GameState.Player;

    //    health = PlayerData.Health;

    //    transform.localScale = new Vector2(PlayerData.posRotScale.x,PlayerData.posRotScale.y);
    //}
}