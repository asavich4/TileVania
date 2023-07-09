using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour{

    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinScore = 10;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(coinScore);
        }
    }
   
}
