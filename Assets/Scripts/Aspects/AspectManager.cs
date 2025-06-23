using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectManager : MonoBehaviour
{
    public static AspectManager Instance;
    
    [Header("Aspect References")]
    [SerializeField] private InputAspect inputAspect;
    [SerializeField] private GameStateAspect gameStateAspect;
    [SerializeField] private ScoringAspect scoringAspect;
    [SerializeField] private CollisionAspect collisionAspect;
    [SerializeField] private SpawnAspect spawnAspect;
    [SerializeField] private MovementAspect movementAspect;
    [SerializeField] private LoggingAspect loggingAspect;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAspects();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeAspects()
    {
        // Crear aspectos si no existen
        if (inputAspect == null)
            inputAspect = gameObject.AddComponent<InputAspect>();
            
        if (gameStateAspect == null)
            gameStateAspect = gameObject.AddComponent<GameStateAspect>();
            
        if (scoringAspect == null)
            scoringAspect = gameObject.AddComponent<ScoringAspect>();
            
        if (collisionAspect == null)
            collisionAspect = gameObject.AddComponent<CollisionAspect>();
            
        if (spawnAspect == null)
            spawnAspect = gameObject.AddComponent<SpawnAspect>();
            
        if (movementAspect == null)
            movementAspect = gameObject.AddComponent<MovementAspect>();
            
        if (loggingAspect == null)
            loggingAspect = gameObject.AddComponent<LoggingAspect>();
        
        // Configurar eventos entre aspectos
        SetupAspectEvents();
        
        LoggingAspect.LogGameEvent("AspectManager", "All aspects initialized successfully");
    }
    
    private void SetupAspectEvents()
    {
        // Input Aspect Events
        InputAspect.OnExitInput += HandleExitInput;
        InputAspect.OnRestartInput += HandleRestartInput;
        
        // Game State Aspect Events
        GameStateAspect.OnGameStateChanged += HandleGameStateChanged;
        GameStateAspect.OnGameOver += HandleGameOver;
        
        // Scoring Aspect Events
        ScoringAspect.OnScoreChanged += HandleScoreChanged;
        ScoringAspect.OnSpeedChanged += HandleSpeedChanged;
        
        // Collision Aspect Events
        CollisionAspect.OnPlayerCollision += HandlePlayerCollision;
        CollisionAspect.OnScoreTrigger += HandleScoreTrigger;
        
        // Spawn Aspect Events
        SpawnAspect.OnObjectSpawned += HandleObjectSpawned;
        SpawnAspect.OnObjectDestroyed += HandleObjectDestroyed;
    }
    
    private void HandleExitInput()
    {
        LoggingAspect.LogPlayerAction("Exit Input");
        Application.Quit();
    }
    
    private void HandleRestartInput()
    {
        if (GameStateAspect.Instance.IsGameOver())
        {
            LoggingAspect.LogPlayerAction("Restart Input");
            GameStateAspect.Instance.SetGameState(GameState.Playing);
        }
    }
    
    private void HandleGameStateChanged(GameState newState)
    {
        LoggingAspect.LogGameState($"State changed to: {newState}");
        
        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
        }
    }
    
    private void HandleGameOver()
    {
        LoggingAspect.LogGameEvent("Game Over");
        ScoringAspect.Instance.ResetScore();
    }
    
    private void HandleScoreChanged(int newScore)
    {
        LoggingAspect.LogScoreChange(0, newScore); // Simplificado para el ejemplo
    }
    
    private void HandleSpeedChanged(float newSpeed)
    {
        LoggingAspect.LogSpeedChange(0, newSpeed); // Simplificado para el ejemplo
    }
    
    private void HandlePlayerCollision(GameObject other)
    {
        LoggingAspect.LogCollision("Player", other.name);
        GameStateAspect.Instance.SetGameState(GameState.GameOver);
    }
    
    private void HandleScoreTrigger(GameObject trigger)
    {
        LoggingAspect.LogGameEvent("Score Trigger", trigger.name);
        ScoringAspect.Instance.AddScore(1f);
    }
    
    private void HandleObjectSpawned(GameObject obj)
    {
        LoggingAspect.LogSpawn(obj.name, obj.transform.position);
    }
    
    private void HandleObjectDestroyed(GameObject obj)
    {
        LoggingAspect.LogGameEvent("Object Destroyed", obj.name);
    }
    
    private void OnDestroy()
    {
        // Limpiar eventos para evitar memory leaks
        if (InputAspect.Instance != null)
        {
            InputAspect.OnExitInput -= HandleExitInput;
            InputAspect.OnRestartInput -= HandleRestartInput;
        }
        
        if (GameStateAspect.Instance != null)
        {
            GameStateAspect.OnGameStateChanged -= HandleGameStateChanged;
            GameStateAspect.OnGameOver -= HandleGameOver;
        }
        
        if (ScoringAspect.Instance != null)
        {
            ScoringAspect.OnScoreChanged -= HandleScoreChanged;
            ScoringAspect.OnSpeedChanged -= HandleSpeedChanged;
        }
        
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.OnPlayerCollision -= HandlePlayerCollision;
            CollisionAspect.OnScoreTrigger -= HandleScoreTrigger;
        }
        
        if (SpawnAspect.Instance != null)
        {
            SpawnAspect.OnObjectSpawned -= HandleObjectSpawned;
            SpawnAspect.OnObjectDestroyed -= HandleObjectDestroyed;
        }
    }
} 