using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour{

    [SerializeField] float bounceForce = 5f;
    [SerializeField] int slimePoints = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null && collision.contacts[0].point.y > transform.position.y)
        {
            FindObjectOfType<GameSession>().AddToScore(slimePoints);
            BouncePlayer(player);
            Destroy(transform.parent.gameObject);
        }
    }

     private void BouncePlayer(PlayerMovement player)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.up * bounceForce;
    }
}





