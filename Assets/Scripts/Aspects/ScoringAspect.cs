using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringAspect : MonoBehaviour
{
    public static ScoringAspect Instance;
    
    // Eventos para cambios de puntuación
    public static System.Action<int> OnScoreChanged;
    public static System.Action OnSpeedIncrease;
    public static System.Action<float> OnSpeedChanged;
    
    [SerializeField] private float baseSpeed = 0.8f;
    [SerializeField] private float increasedSpeed = 0.2f;
    [SerializeField] private int pipesNeededForSpeedIncrease = 10;
    
    private float currentScore = 0f;
    private float currentSpeed;
    private TextMeshProUGUI scoreText;
    
    public float CurrentScore => currentScore;
    public float CurrentSpeed => currentSpeed;
    
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
    
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        currentSpeed = baseSpeed;
        
        if (scoreText != null)
        {
            UpdateScoreDisplay();
        }
    }
    
    private void Update()
    {
        if (scoreText != null)
        {
            UpdateScoreDisplay();
        }
    }
    
    public void AddScore(float points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke((int)currentScore);
        
        // Verificar si debe aumentar la velocidad
        if (currentScore % pipesNeededForSpeedIncrease == 0)
        {
            IncreaseSpeed();
        }
    }
    
    private void IncreaseSpeed()
    {
        currentSpeed += increasedSpeed;
        OnSpeedIncrease?.Invoke();
        OnSpeedChanged?.Invoke(currentSpeed);
        Debug.Log($"Velocidad aumentada a: {currentSpeed}");
    }
    
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString("0");
        }
    }
    
    public void ResetScore()
    {
        currentScore = 0f;
        currentSpeed = baseSpeed;
        OnScoreChanged?.Invoke(0);
        OnSpeedChanged?.Invoke(currentSpeed);
    }
    
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
} 