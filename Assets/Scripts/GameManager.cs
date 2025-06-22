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
    }
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        GameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
