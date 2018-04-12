using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handles animations
public class AnimationHandler : MonoBehaviour {

    public SpriteRenderer entityRenderer;
    public AudioSource soundPlayer;

    //gets ground from entity
    public bool isGrounded, entityType;
    //animation checkers
    bool isMoving, isSpinning;

    //animation variables
    public bool idle, walking, running, falling, jumping, ducking, lookingUp, flipped, spinning;

    int runFrame, walkFrame, spinFrame;
    
    float timer = 0.0f;

    //sprites
    public Sprite Idle, Duck, LookUp, Jump, RunJump, Fall, FaceForeward, FaceBackward;
    public Sprite[] Walk, Run;

    public AudioClip jump;
    public AudioClip spinJump;

    // Use this for initialization
    void Start() {
        //component getters
        entityRenderer = GetComponent<SpriteRenderer>();
        soundPlayer = GetComponent<AudioSource>();
    }


    //--------------------allows animations to start again--------------------//
    //sets player to no longer moving
    void setNotMoving() {
        isMoving = false;
    }
    //sets player to no longer spining
    void setNotSpinning() {
        isSpinning = false;
    }


    //--------------------Internal animation voids--------------------//
    void SpinAnim() {

        if (spinning && !isSpinning)
        {
            isSpinning = true;
            switch (spinFrame) {
                case 0:
                    entityRenderer.sprite = Idle;
                    flipped = !flipped;
                    break;
                case 1:
                    entityRenderer.sprite = FaceBackward;
                    break;
                case 2:
                    entityRenderer.sprite = Idle;
                    flipped = !flipped;
                    break;
                case 3:
                    entityRenderer.sprite = FaceForeward;
                    break;
                default:
                    Debug.LogError("Frame " + walkFrame + " is not valid for the spinning animation.");
                    break;
            }
            spinFrame++;
            if (spinFrame > 3) spinFrame = 0;
            Invoke("setNotSpinning", 0.05f);
        }
    }

    //TODO fix buggy animations
    void WalkAnim() {
        if (isGrounded && !isMoving) {
            isMoving = true;
            entityRenderer.sprite = Walk[walkFrame];
            walkFrame++;

            if (walkFrame >= Run.Length) walkFrame = 0;
            Invoke("setNotMoving", 0.1f);
        }
    }

    //running animation
    void RunAnim() {
        //if maio is grounded and a moving animation is not running, run animation
        if (isGrounded && !isMoving) {
            //animation is running
            isMoving = true;
            entityRenderer.sprite = Run[runFrame];
            runFrame++;

            if (runFrame >= Run.Length) runFrame = 0;
            Invoke("setNotMoving", 0.1f);
        }
    }


        //--------------------Animators to call in class--------------------//
    public void PlayerAnimate() {
        //if spinning, apply spinning anim and sound
        if (spinning) {
            soundPlayer.clip = spinJump;
            SpinAnim();
        }
        else if (falling) {
            if (running) entityRenderer.sprite = RunJump;
            else entityRenderer.sprite = Fall;
        }
        else if (jumping) {
            soundPlayer.clip = jump;
            if (running) entityRenderer.sprite = RunJump;
            else entityRenderer.sprite = Jump;
        }
        //if running, start running animation
        else if (running) RunAnim();
        else if (walking) WalkAnim();
        else if (idle) entityRenderer.sprite = Idle;
        else
        {
            if (ducking) entityRenderer.sprite = Duck;
            if (lookingUp) entityRenderer.sprite = LookUp;
        }

    }

    public void KoopaAnimate() {
        if (walking) WalkAnim();
    }

    public void FireballAnimate() {
        timer += Time.deltaTime;
        if(timer >= .05) {
            transform.Rotate(Vector3.forward * 90);
            timer = 0;
        }
    }

    public void Flip() {
        //flips sprite renderer
        if (flipped) entityRenderer.flipX = true;
        else entityRenderer.flipX = false;
    }

}
