using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    [SerializeField] float delay = 1f;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath(){
        StartCoroutine(DelayRestart());
    }

    IEnumerator DelayRestart(){
        yield return new WaitForSeconds(delay);
        if(playerLives > 1){
            TakeLife();
        }
        else{
            ResetGameSession();
        }
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife(){
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
