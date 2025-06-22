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
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}