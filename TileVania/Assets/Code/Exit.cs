using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float delay = 1;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        StartCoroutine(LoadTimer());
        }
    }

    IEnumerator LoadTimer(){
        yield return new WaitForSecondsRealtime(delay);
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
