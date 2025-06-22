using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    private ScorePointPipes scorePointPipes;

    private void Start()
    {
        scorePointPipes = FindObjectOfType<ScorePointPipes>();
    }

    private void Update()
    {
        // Usar la velocidad actual del ScorePointPipes
        float currentSpeed = scorePointPipes.GetCurrentSpeed();
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }
}
