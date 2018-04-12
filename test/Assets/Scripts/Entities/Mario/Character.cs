using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity {
    //checks if mario can stop his jump early, checks if mario has jumped
    bool canStopAscent, jumpSoundPlayed;

    //collects collectibles
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Trigger Detected: " + collision.gameObject);
        if (collision.gameObject.tag == "Collectible") {

            Collectible collect = collision.GetComponent<Collectible>();

            if (collect) {
                Debug.Log("Collected " + collect.points + " points");
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Powerup_jump") {
            jumpforce += jumpforceBoost;
            StartCoroutine("StopJumpBoost");
            Destroy(collision.gameObject);
        }
    }
    IEnumerator StopJumpBoost() {
        yield return new WaitForSeconds(jumpforceBoostTime);
        jumpforceBoost /= jumpforceBoost;
    }
    //pawameta
    //initiates run
    IEnumerator PMeter() {
        if (speed < pSpeed) {
            speed += 0.04f;
        }
        yield return new WaitForSeconds(0.01f);
    }
    //Use this for initialization
    void Start() {
        Body = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
        jumpSoundPlayed = true;
        anim.lookingUp = false;
        anim.ducking = false;

        if (baseSpeed <= 0){
            baseSpeed = 3.0f;
        }

        if(baseJumpForce <= 0) {
            baseJumpForce = 320.0f;
        }
    }
    //Update is called once per frame
    override protected void Update () {
        base.Update();

        anim.PlayerAnimate();

        float moveValue = Input.GetAxisRaw("Horizontal");

        Body.velocity = new Vector2(moveValue * speed, Body.velocity.y);

        //sets animation bools to temp bools
        anim.isGrounded = isGrounded;

        if (Input.GetKeyDown(KeyCode.W)) anim.ducking = false;
        //looking up action + animation
        if (Input.GetKey(KeyCode.W)) anim.lookingUp = true;
        else anim.lookingUp = false;


        if (Input.GetKey(KeyCode.S)) anim.lookingUp = false;
        //ducking action + animation
        if (Input.GetKey(KeyCode.S)) anim.ducking = true;
        else anim.ducking = false;

        //for ending jumps early
        if (isGrounded) {
            jumpSoundPlayed = false;
            canStopAscent = true;
        }

        //makes it so that the jump sound can not be played before actually jumping and turns off spin jump animation
        if (isGrounded && Body.velocity.y <= 0) {
            jumpSoundPlayed = true;
            anim.spinning = false;
        }

        //todo fix bugy jumps
        //starts spin jump
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded) {
            Body.AddForce(Vector2.up * spinjumpforce, ForceMode2D.Force);
            jumpSoundPlayed = false;
            anim.spinning = true;
            anim.soundPlayer.clip = anim.spinJump;
        }

        //starts jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Body.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
            jumpSoundPlayed = false;
        }

        //checks if you can end your jump prematurely
        if (Input.GetButtonUp("Jump") && !isGrounded && canStopAscent && Body.velocity.y > 0 || Input.GetKeyUp(KeyCode.LeftControl) && !isGrounded && canStopAscent && Body.velocity.y > 0) {
            if (!jumpSoundPlayed){
                anim.soundPlayer.Play();
                jumpSoundPlayed = true;
            }
            Body.velocity = new Vector2(moveValue * speed, 0);
            canStopAscent = false;
        }
        //if the jump sound has not been played yet and mario is falling
        if (!jumpSoundPlayed && Body.velocity.y < 0 && !isGrounded) {
            anim.soundPlayer.Play();
            jumpSoundPlayed = true;
        }

        //check if ascending through air, set sprite to jumping
        if (Body.velocity.y > 0 && !isGrounded) {
            anim.jumping = true;
        }

        //check if falling through air, set sprite to falling
        else if (Body.velocity.y < 0 && !isGrounded) {
            anim.jumping = false;
            anim.falling = true;
        }
        //if moving
        else if (Body.velocity.x != 0) {
            jumpforce = 8.5f * Mathf.Abs(Body.velocity.x) + baseJumpForce;

            spinjumpforce = 8.5f * Mathf.Abs(Body.velocity.x) + baseSpinJumpForce;

            anim.walking = true;

            if (Input.GetKey(KeyCode.LeftShift)) StartCoroutine(PMeter());
        }

        //else, rests jumpforces
        else {
            jumpforce = baseJumpForce;
            spinjumpforce = baseSpinJumpForce;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            StopCoroutine(PMeter());
            speed = baseSpeed;
        }

        //sets idle
        if (Body.velocity.y == 0 && Body.velocity.x == 0 && !anim.ducking && !anim.lookingUp && !anim.spinning) anim.idle = true;

        //sets not idle
        else anim.idle = false;

        if (speed >= pSpeed) {
            anim.running = true;
            anim.walking = false;
        }

        else anim.running = false;

        if(Body.velocity.y == 0) {
            anim.falling = false;
            anim.jumping = false;
        }
        //resets speed and animations
        if (Body.velocity.x == 0) {
            anim.walking = false;
            anim.running = false;

            speed = baseSpeed;
        }
        if (!anim.spinning) {
            //swaps facing direction
            Flip();
        }
    }
}