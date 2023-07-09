using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float delay = 1;
    [SerializeField] int exitScore = 100;
    [SerializeField] float volume;
    [SerializeField] AudioClip exitSound;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        FindObjectOfType<GameSession>().AddToScore(exitScore);
        AudioSource.PlayClipAtPoint(exitSound, Camera.main.transform.position, volume);
        StartCoroutine(LoadTimer());
        }
    }

    IEnumerator LoadTimer(){
        yield return new WaitForSecondsRealtime(delay);
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
