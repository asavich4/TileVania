using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
     CapsuleCollider2D myCapsuleCollider2D;
     BoxCollider2D boxFlip;
     Rigidbody2D myRigidbody;

     [SerializeField] float slimeSpeed;
    void Start()
    {
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
         myRigidbody = GetComponent<Rigidbody2D>();
         boxFlip = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Walk(); 
    }

    void SpritFlip(){
        slimeSpeed = -slimeSpeed;
        transform.localScale = new Vector2 (-(Mathf.Sign( myRigidbody.velocity.x)), 1f);
    }
     void OnTriggerExit2D(Collider2D other) {
        SpritFlip();
    }

    void Walk(){
        bool move = true;
        if(move){
            myRigidbody.velocity = new Vector2(slimeSpeed, 0);
        }
        
    }
}

