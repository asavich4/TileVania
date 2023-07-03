using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour
{
    Slime slime;
    Rigidbody2D slimeRigidbody;
    CapsuleCollider2D slimeCollider;
    [SerializeField] float bounceForce = 5f;

    private void Start()
    {
        slimeRigidbody = transform.parent.GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayerJumpingOnSlime(collision))
        {
            BouncePlayer();
            SlimeActions();
            //slime.slimeLife = false; 
        }
    }

    private bool IsPlayerJumpingOnSlime(Collision2D collision)
    {
        // Check if the other collider belongs to the player and if the player is above the slime object
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null && player.transform.position.y > transform.parent.position.y)
        {
            slimeCollider.enabled = false;
            return true;
        }
        return false;
    }

    private void BouncePlayer()
    {
        // Apply an upward force to the player to simulate a bounce
        Rigidbody2D playerRigidbody = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
        playerRigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    private void SlimeActions()
    {
        // Trigger any desired animation or effects for the slime's death
        Animator slimeAnimator = transform.parent.GetComponent<Animator>();
        slimeAnimator.SetBool("isDead", true);

        Destroy(transform.parent.gameObject, 0f);
    }
}


