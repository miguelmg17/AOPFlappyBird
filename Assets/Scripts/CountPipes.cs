using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPipes : MonoBehaviour
{
    private void Start()
    {
        scorePointPipes = FindObjectOfType<ScorePointPipes>();
        
        // Suscribirse a eventos de aspectos
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.OnScoreTrigger += HandleScoreTrigger;
        }
    }
    
    private ScorePointPipes scorePointPipes;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scorePointPipes.scoreUp(1f);
            
            // Notificar al aspecto de colisiones
            if (CollisionAspect.Instance != null)
            {
                CollisionAspect.Instance.RegisterScoreTrigger(gameObject);
            }
        }
    }
    
    private void HandleScoreTrigger(GameObject trigger)
    {
        // Lógica adicional cuando se activa un trigger de puntuación
        if (trigger == gameObject && LoggingAspect.Instance != null)
        {
            LoggingAspect.LogGameEvent("Score Trigger Activated", gameObject.name);
        }
    }
    
    private void OnDestroy()
    {
        // Limpiar suscripciones
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.OnScoreTrigger -= HandleScoreTrigger;
        }
    }
}
