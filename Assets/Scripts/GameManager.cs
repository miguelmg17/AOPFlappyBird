using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject GameOverCanvas;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Exiting");
            Application.Quit();
        }
        if (GameOverCanvas.activeSelf && Input.GetKey(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
        
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.OnGameOver += HandleGameOverFromAspect;
        }
    }
    
    private void HandleGameOverFromAspect()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.Instance.SetGameState(GameState.GameOver);
        }
    }
    
    public void RestartGame()
    {
        GameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.Instance.SetGameState(GameState.Playing);
        }
    }
    
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
        
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.Instance.SetGameState(GameState.Menu);
        }
    }
    
    private void OnDestroy()
    {
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.OnGameOver -= HandleGameOverFromAspect;
        }
    }
}
