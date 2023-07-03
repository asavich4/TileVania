using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null && collision.contacts[0].point.y > transform.position.y)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}





