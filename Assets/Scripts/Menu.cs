using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject GameMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Play();
        }
    }
    
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        // Notificar al aspecto de estado
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.Instance.SetGameState(GameState.Playing);
        }
        
        // Logging del aspecto
        if (LoggingAspect.Instance != null)
        {
            LoggingAspect.LogPlayerAction("Menu Play Button");
        }
    }
}
