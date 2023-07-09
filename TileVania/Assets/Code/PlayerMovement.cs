using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myFeetCollider2D;

    [SerializeField] GameObject Arrow;
    [SerializeField]Transform Bow;

    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed;
    [SerializeField] float deathBounce;
    public bool playerLife = true;
     
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(playerLife == true){
        Run();
        FlipSprite();
        Ladder();
        Death();
        }
        else{
            return;
        }
    }
    
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void Death(){
        if( myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Slime")) ||  myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("DeathTouch")) || myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("DeathTouch"))){
            playerLife = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity += new Vector2(0f, deathBounce);
        }
    }

    void OnJump(InputValue Value){
        if(Value.isPressed && myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){
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
        float pauseCheck = Input.GetAxis("Vertical");

        if(myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, (moveInput.y * runSpeed));
            myRigidbody.velocity = climbVelocity;
            playerHasVerticalSpeed = true;
            myRigidbody.gravityScale = 0;
            if(pauseCheck == 0){
                playerHasVerticalSpeed = false;
            }

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

    void OnFire(InputValue value){
        Instantiate(Arrow, Bow.position, transform.rotation);
        myAnimator.SetBool("isShooting", true);
        StartCoroutine(ResetShootingAnimation());
    }

    IEnumerator ResetShootingAnimation(){
        float animationDuration = 0.25f;
        yield return new WaitForSeconds(animationDuration);
        myAnimator.SetBool("isShooting", false);
    }

    void OnRespawn(InputValue value){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
}