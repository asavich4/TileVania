using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidbody2d;
    BoxCollider2D arrowBox;
    PlayerMovement player;
    float playerDirection;
    [SerializeField] float xArrow;
    [SerializeField] float yArrow;

    [SerializeField] int slimePoints = 10;

    void Start()
    {
        myRigidbody2d = GetComponentInChildren<Rigidbody2D>();
        arrowBox = GetComponentInChildren<BoxCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        playerDirection = player.transform.localScale.x * xArrow;
    }

    void Update()
    {
        ArrowMovement();
        FlipSprite();
    }

    void ArrowMovement()
    {
        myRigidbody2d.velocity = new Vector2(playerDirection, yArrow);
    }

    void FlipSprite()
    {
        bool arrowFace = Mathf.Abs(myRigidbody2d.velocity.x) > Mathf.Epsilon;
        if (arrowFace)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2d.velocity.x), 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Slime"){
            FindObjectOfType<GameSession>().AddToScore(slimePoints);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Slime")){
            FindObjectOfType<GameSession>().AddToScore(slimePoints);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}

