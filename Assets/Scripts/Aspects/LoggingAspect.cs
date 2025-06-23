using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingAspect : MonoBehaviour
{
    public static LoggingAspect Instance;
    
    [SerializeField] private bool enableLogging = true;
    [SerializeField] private bool logToFile = false;
    
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
    
    public static void LogGameEvent(string eventName, object data = null)
    {
        if (Instance != null && Instance.enableLogging)
        {
            string message = $"[GAME EVENT] {eventName}";
            if (data != null)
            {
                message += $" - Data: {data}";
            }
            Debug.Log(message);
        }
    }
    
    public static void LogPlayerAction(string action)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[PLAYER ACTION] {action}");
        }
    }
    
    public static void LogGameState(string state)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[GAME STATE] {state}");
        }
    }
    
    public static void LogScoreChange(int oldScore, int newScore)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[SCORE] {oldScore} -> {newScore}");
        }
    }
    
    public static void LogSpeedChange(float oldSpeed, float newSpeed)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[SPEED] {oldSpeed:F2} -> {newSpeed:F2}");
        }
    }
    
    public static void LogCollision(string object1, string object2)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[COLLISION] {object1} <-> {object2}");
        }
    }
    
    public static void LogSpawn(string objectType, Vector3 position)
    {
        if (Instance != null && Instance.enableLogging)
        {
            Debug.Log($"[SPAWN] {objectType} at {position}");
        }
    }
    
    public static void LogError(string error, string context = "")
    {
        string message = $"[ERROR] {error}";
        if (!string.IsNullOrEmpty(context))
        {
            message += $" - Context: {context}";
        }
        Debug.LogError(message);
    }
    
    public static void LogWarning(string warning, string context = "")
    {
        string message = $"[WARNING] {warning}";
        if (!string.IsNullOrEmpty(context))
        {
            message += $" - Context: {context}";
        }
        Debug.LogWarning(message);
    }
} 