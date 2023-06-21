using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
     [SerializeField] int playerLives = 3;
    [SerializeField] int score =0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        
        if (numGameSessions > 1)
        {
           Destroy(gameObject);
        }
        else
        {
          DontDestroyOnLoad(gameObject);
        }
    }

    
    public void ProcessPlayerDeath()
    {
      if (playerLives > 1)
      {
         TakeLife();
      }
      else 
      {
        RestGameSession();
      }
    }
    

  void TakeLife()
  {
    playerLives--;
    int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScenceIndex);
  }

  void RestGameSession()
  {
      SceneManager.LoadScene(0);
  }
}
