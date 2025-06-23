using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePointPipes : MonoBehaviour
{
    private float scorePipes = 0f;
    private TextMeshProUGUI textMeshScore;
    private float currentSpeed;
    [SerializeField] private float baseSpeed = 0.8f;
    [SerializeField] private float aumentedSpeed = 0;
    [SerializeField] private float amountPipesNeeded = 10f;
    
    private void Start()
    {
        textMeshScore = GetComponent<TextMeshProUGUI>();
        currentSpeed = baseSpeed;
        
        // Suscribirse a eventos de aspectos
        if (ScoringAspect.Instance != null)
        {
            ScoringAspect.OnScoreChanged += HandleScoreChanged;
            ScoringAspect.OnSpeedChanged += HandleSpeedChanged;
        }
    }

    private void Update()
    {
        textMeshScore.text = scorePipes.ToString("0");
    }

    public void scoreUp(float score)
    {
        scorePipes += score;

        if (scorePipes % amountPipesNeeded == 0)
        {
            currentSpeed += aumentedSpeed;
            Debug.Log($"Velocidad aumentada a: {currentSpeed}");
        }
        
        // Notificar al aspecto de scoring
        if (ScoringAspect.Instance != null)
        {
            ScoringAspect.Instance.AddScore(score);
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    
    private void HandleScoreChanged(int newScore)
    {
        // Sincronizar con el aspecto de scoring
        scorePipes = newScore;
        
        // Logging del aspecto
        if (LoggingAspect.Instance != null)
        {
            LoggingAspect.LogScoreChange((int)(scorePipes - 1), newScore);
        }
    }
    
    private void HandleSpeedChanged(float newSpeed)
    {
        // Sincronizar con el aspecto de scoring
        currentSpeed = newSpeed;
        
        // Logging del aspecto
        if (LoggingAspect.Instance != null)
        {
            LoggingAspect.LogSpeedChange(currentSpeed - aumentedSpeed, newSpeed);
        }
    }
    
    private void OnDestroy()
    {
        // Limpiar suscripciones
        if (ScoringAspect.Instance != null)
        {
            ScoringAspect.OnScoreChanged -= HandleScoreChanged;
            ScoringAspect.OnSpeedChanged -= HandleSpeedChanged;
        }
    }
}