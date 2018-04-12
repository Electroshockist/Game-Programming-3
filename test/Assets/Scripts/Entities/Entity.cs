using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Rigidbody2D Body;
    public AnimationHandler anim; 

    //ground related
    //[HideInInspector] (disabled for testing)
    public bool isGrounded, leftTriggered, rightTriggered, upTriggered;
    public LayerMask isGroundLayer;
    public Transform groundCheck, leftCheck, rightCheck, upCheck;
    public float groundCheckRadius;
    
    //base forces
    public float baseSpeed, baseJumpForce;
    public const float baseSpinJumpForce = 280.0f;
    public const float pSpeed = 5f;

    //current force variables
    public float speed;
    public float jumpforce;
    public float spinjumpforce;

    public float jumpforceBoost;
    public float jumpforceBoostTime;
    
    void SetPhysicsDefaults(){
        //component getters
        anim = GetComponent<AnimationHandler>();

        //setters
        if (!Body) {
            Debug.LogError("Rigidbody2D not found.");
        }
        else {
            Body.constraints = RigidbodyConstraints2D.FreezeRotation;
            Body.mass = 1.0f;
        }

        if (speed <= 0) {
            speed = baseSpeed;
            Debug.Log("Speed not set. Setting speed to " + speed);
        }

        if (jumpforce <= 0.0f) {
            jumpforce = baseJumpForce;
            Debug.Log("Jump force not set. Setting jump force to " + jumpforce);
        }

        if (spinjumpforce <= 0.0f) {
            spinjumpforce = baseSpinJumpForce;
            Debug.Log("Jump force not set. Setting jump force to " + jumpforce);
        }

        if (jumpforceBoost <= 0.0f) {
            jumpforceBoost = 10.5f;
            Debug.Log("Jump force boost not set. Setting jump force boost to " + jumpforceBoost);
        }

        if (jumpforceBoostTime <= 0.0f) {
            jumpforceBoostTime = 2.0f;
            Debug.Log("Jump force boost time not set. Setting jump force boost time to " + jumpforceBoostTime);
        }

        if (!groundCheck) {
            Debug.Log("GroundCheck not found.");
            groundCheck = GetComponent<Transform>();
        }
        if (!leftCheck)
        {
            Debug.Log("LeftCheck not found.");
            leftCheck = GetComponent<Transform>();
        }

        if (!rightCheck)
        {
            Debug.Log("RightCheck not found.");
            rightCheck = GetComponent<Transform>();
        }
        if (!upCheck)
        {
            Debug.Log("UpCheck not found.");
            upCheck = GetComponent<Transform>();
        }

        if (groundCheckRadius <= 0.0f) {
            groundCheckRadius = 0.1f;
            Debug.Log("groundCheckRadius not set. Setting groundCheckRadius to " + groundCheckRadius);
        }

        
    }
    public void Flip() {
        //swaps facing direction
        if (Body.velocity.x < 0) anim.flipped = true;
        else if (Body.velocity.x > 0)  anim.flipped = false;
    }

    //find out why start doesn't work properly
    //start
    void Awake() {
        SetPhysicsDefaults();
    }
    virtual protected void Update() {
        anim.KoopaAnimate();
        anim.Flip();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}