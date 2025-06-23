using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    private ScorePointPipes scorePointPipes;

    private void Start()
    {
        scorePointPipes = FindObjectOfType<ScorePointPipes>();
        
        // Suscribirse a eventos de aspectos
        if (MovementAspect.Instance != null)
        {
            MovementAspect.OnObjectMoved += HandleObjectMoved;
        }
    }

    private void Update()
    {
        // Usar la velocidad actual del ScorePointPipes
        float currentSpeed = scorePointPipes.GetCurrentSpeed();
        
        // Usar el aspecto de movimiento si está disponible
        if (MovementAspect.Instance != null)
        {
            MovementAspect.Instance.MoveObjectConstant(gameObject, Vector3.left, currentSpeed);
        }
        else
        {
            // Mantener la lógica original como fallback
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        }
    }
    
    private void HandleObjectMoved(GameObject obj, Vector3 movement)
    {
        // Lógica adicional cuando se mueve un objeto
        if (obj == gameObject && LoggingAspect.Instance != null)
        {
            LoggingAspect.LogGameEvent("Pipe Movement", movement);
        }
    }
    
    private void OnDestroy()
    {
        // Limpiar suscripciones
        if (MovementAspect.Instance != null)
        {
            MovementAspect.OnObjectMoved -= HandleObjectMoved;
        }
    }
}
