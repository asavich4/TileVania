using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour{

    [SerializeField] float bounceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null && collision.contacts[0].point.y > transform.position.y)
        {
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





