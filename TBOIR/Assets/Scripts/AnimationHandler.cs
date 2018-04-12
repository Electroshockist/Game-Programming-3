﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    //checks body type for animations
    public BodyType type = new BodyType();

    public Sprite Forward, Backward, Horizontal, ForwardCrying, BackwardCrying, HorizontalCrying, ItemAquire;

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
    }
    // Update is called once per frame
    void Update () {
        SetSurrogates();

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
//TODO: figure out how to implement
public class BodyType {
    public SpriteRenderer headSprite, bodySprite;
    //TODO create sub classes
    public Sprite Forward, Backward, Horizontal, ForwardCrying, BackwardCrying, HorizontalCrying, ItemAquire;

    //head and body object
    public GameObject headObject, bodyObject;
    public Animator bodyAnim;

    //TODO: add failsafes for multiple bodytypes
    //[hideininspector]
    public bool humanoid, player;
   
    //[HideInInspector]
    public bool crying;

    //decides whether to use facing direction or moving direction
    public ControlType facing = new ControlType();
    public ControlType moving = new ControlType();
    
    //TODO
    public void isPlayer() {
        humanoid = true;
    }
    public void isHumanoid() {
        //if you are not pressing a firing button, face foreward and reset animations
        if (facing.AllFalse()) {
            facing.forward = true;

            crying = false;
            facing.right = false;
            headSprite.flipX = false;
            facing.backward = false;
        }
        //if you've aquired an item
        if (bodyAnim.GetBool("Item Aquire")) {
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