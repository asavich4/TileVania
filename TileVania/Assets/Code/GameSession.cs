using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    [SerializeField] float delay = 1f;
    [SerializeField] int playerScore = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
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

    void Start(){
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
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
    public void AddToScore(int pointsToAdd){
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
        ScoreToLife();
    }

    void ScoreToLife(){
        if(playerScore >= 500){
            playerScore -=500;
            playerLives++;
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
        livesText.text = playerLives.ToString();
    }
}
