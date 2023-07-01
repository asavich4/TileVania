using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D boxFlip;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    [SerializeField] float slimeSpeed;
    
    public bool slimeLife = true;

    void Start()
    {
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        boxFlip = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update(){
        if(slimeLife){
        Walk();
        }
    }

    void SpritFlip()
    {
        slimeSpeed = -slimeSpeed;
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    private void OnTriggerExit2D(Collider2D other){
        SpritFlip();
    }

    
    void Walk()
    {
        myRigidbody.velocity = new Vector2(slimeSpeed, myRigidbody.velocity.y);
    }

}

