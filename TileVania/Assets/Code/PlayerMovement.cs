using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed;
     
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Ladder();
    }
    
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue Value){
        if(Value.isPressed && myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void Ladder(){
        bool playerHasVerticalSpeed = false;

        if(myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, (moveInput.y * runSpeed));
            myRigidbody.velocity = climbVelocity;
            playerHasVerticalSpeed = true;
            myRigidbody.gravityScale = 0;
        }
        if(!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            playerHasVerticalSpeed = false;
            myRigidbody.gravityScale = 3;
        }

        myAnimator.SetBool("isCliming", playerHasVerticalSpeed);
    }

    void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
        transform.localScale = new Vector2 (Mathf.Sign( myRigidbody.velocity.x), 1f);
        }
    }
}
