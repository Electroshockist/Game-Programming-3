using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    //checks body type for animations
    public BodyType type = new BodyType();

    public Sprite Forward, Backward, Horizontal, ForwardCrying, BackwardCrying, HorizontalCrying, ItemAquire;

    public Sprite[] Invincible;

    //head and body object
    public GameObject headObject, bodyObject;

    void SetSurrogates() {
        type.Forward = Forward;
        type.Backward = Backward;
        type.Horizontal = Horizontal;
        type.ForwardCrying = ForwardCrying;
        type.BackwardCrying = BackwardCrying;
        type.HorizontalCrying = HorizontalCrying;
        type.ItemAquire = ItemAquire;
        type.Invincible = Invincible;
    }
    // Update is called once per frame
    void Update () {
        SetSurrogates();
        type.bodyAnim.speed = 1;

        if (type.humanoid) {
            type.isHumanoid();
        }

        if (type.player) {
            type.isPlayer();
        }
    }
}

//for differentiating between body and head
public class ControlType {
    [HideInInspector]
    public bool forward, backward, left, right;
    [HideInInspector]
    public bool AllFalse() {
        if (forward || backward || left || right) return false;
        else return true;
    }
    public void ForceFacing(string dir) {
        //makes sure you can't face two bodies at once
        switch (dir) {
            case "forward":
                forward = true;
                backward = false;
                left = false;
                right = false;
                break;

            case "backward":
                forward = false;
                backward = true;
                left = false;
                right = false;
                break;

            case "left":
                forward = false;
                backward = false;
                left = true;
                right = false;
                break;

            case "right":
                forward = false;
                backward = false;
                left = false;
                right = true;
                break;

            case "none":
                forward = false;
                backward = false;
                left = false;
                right = false;
                break;
            default:
                Debug.LogError(dir + " is not a direction.");
                break;
        }
    }
}

//for future-proofing when entities of different body types come into play
public class BodyType {
    public SpriteRenderer headSprite, bodySprite;
    //TODO create sub classes
    public Sprite Forward, Backward, Horizontal, ForwardCrying, BackwardCrying, HorizontalCrying, ItemAquire;
    public Sprite[] Invincible;

    //head and body object
    public GameObject headObject, bodyObject;
    public Animator bodyAnim;

    //TODO: add failsafes for multiple bodytypes
    //[hideininspector]
    public bool humanoid, player, crying, aquireItem, dead, invincible;

    //decides whether to use facing direction or moving direction
    public ControlType facing = new ControlType();
    public ControlType moving = new ControlType();

    //TODO: streamline effects
    float invincibleTime = 7.0f;
    float animationTime = 0;

    void Invincibility() {
        float frametime = 0.05f;

        if (animationTime < frametime * 6) {
            //animationTime = 0;
            Debug.Log(frametime * 5);
            Debug.Log(animationTime);

        }
        else if (animationTime < frametime * 5) {
            headSprite.sprite = Invincible[4];
            Debug.Log(Invincible[4]);
        }
        else if (animationTime < frametime * 4) {
            headSprite.sprite = Invincible[3];
            Debug.Log(frametime * 4);
        }
        else if (animationTime < frametime * 3) {
            headSprite.sprite = Invincible[2];
            Debug.Log(frametime * 3);
        }

        else if (animationTime < frametime * 2) {
            headSprite.sprite = Invincible[1];
            Debug.Log(frametime * 2);
        }
        else if (animationTime < frametime) {
            bodySprite.sprite = Invincible[0];
            Debug.Log(frametime);
        }
    }

    //TODO
    public void isPlayer() {humanoid = true;}

    public void isHumanoid() {

        if (dead) bodyAnim.SetBool("Dead", true);
        else {
            //if the player has aquired an item
            bodyAnim.SetBool("Item Aquire", aquireItem);
            //if moving vertically
            if (moving.forward || moving.backward) bodyAnim.SetBool("isWalkingVertical", true);
            else bodyAnim.SetBool("isWalkingVertical", false);

            //if moving horizontally
            if (moving.left || moving.right) {
                bodyAnim.SetBool("isWalkingHorizontal", true);
                //if moving left
                if (moving.left) bodySprite.flipX = true;
                else bodySprite.flipX = false;
            }
            else {
                bodyAnim.SetBool("isWalkingHorizontal", false);
                bodySprite.flipX = false;
            }

            //if you are not pressing a firing button, face foreward and reset animations
            if (!bodyAnim.GetBool("Item Aquire")) {
                if (facing.AllFalse()) {
                    if (moving.AllFalse()) {
                        moving.ForceFacing("forward");
                        facing.ForceFacing("forward");
                    }
                    facing.forward = moving.forward;
                    facing.backward = moving.backward;
                    facing.left = moving.left;
                    facing.right = moving.right;

                    crying = false;
                    headSprite.flipX = false;
                }
            }

            if (invincible) {
                Invincibility();
                invincibleTime -= Time.deltaTime;
                animationTime += Time.deltaTime;
                if (invincibleTime <= 0) {
                    invincible = false;
                    invincibleTime = 0.7f;
                    animationTime = 0;
                }
            }
            //if you've aquired an item
            else if (bodyAnim.GetBool("Item Aquire")) {
                bodySprite.sprite = ItemAquire;
                headSprite.enabled = false;
            }
            else {
                headSprite.enabled = true;
                //checks if crying
                if (crying) {
                    //checks facing directions
                    if (facing.forward) headSprite.sprite = ForwardCrying;
                    if (facing.backward) headSprite.sprite = BackwardCrying;
                    if (facing.right) headSprite.sprite = HorizontalCrying;
                    if (facing.left) {
                        headSprite.sprite = HorizontalCrying;
                        headSprite.flipX = true;
                    }
                    else headSprite.flipX = false;
                }
                else {
                    //checks facing directions if not crying
                    if (facing.forward) headSprite.sprite = Forward;
                    if (facing.backward) headSprite.sprite = Backward;
                    if (facing.right) headSprite.sprite = Horizontal;
                    if (facing.left) {
                        headSprite.sprite = Horizontal;
                        headSprite.flipX = true;
                    }
                    else headSprite.flipX = false;
                }
            }
        }
    }
}