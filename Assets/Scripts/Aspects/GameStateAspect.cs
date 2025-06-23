using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    GameOver,
    Paused
}

public class GameStateAspect : MonoBehaviour
{
    public static GameStateAspect Instance;
    
    // Eventos para cambios de estado
    public static System.Action<GameState> OnGameStateChanged;
    public static System.Action OnGameStart;
    public static System.Action OnGameOver;
    public static System.Action OnGameRestart;
    public static System.Action OnGamePause;
    public static System.Action OnGameResume;
    
    private GameState currentState = GameState.Menu;
    
    public GameState CurrentState => currentState;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetGameState(GameState newState)
    {
        if (currentState != newState)
        {
            GameState previousState = currentState;
            currentState = newState;
            
            OnGameStateChanged?.Invoke(newState);
            
            // Invocar eventos específicos según el estado
            switch (newState)
            {
                case GameState.Playing:
                    if (previousState == GameState.Menu)
                        OnGameStart?.Invoke();
                    else if (previousState == GameState.Paused)
                        OnGameResume?.Invoke();
                    break;
                case GameState.GameOver:
                    OnGameOver?.Invoke();
                    break;
                case GameState.Paused:
                    OnGamePause?.Invoke();
                    break;
            }
        }
    }
    
    public bool IsState(GameState state)
    {
        return currentState == state;
    }
    
    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }
    
    public bool IsGameOver()
    {
        return currentState == GameState.GameOver;
    }
} 