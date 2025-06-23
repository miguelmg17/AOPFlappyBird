using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private float birdSpeed = 1.5f;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Suscribirse a eventos de aspectos
        if (InputAspect.Instance != null)
        {
            InputAspect.OnJumpInput += HandleJumpInput;
        }
        
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.OnPlayerCollision += HandlePlayerCollision;
        }
    }

    private void Update()
    {
        // Mantener la lógica original de input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Mouse.current.leftButton.wasPressedThisFrame)
        {
            rb2d.velocity = Vector2.up * birdSpeed;
        }
    }
    
    private void HandleJumpInput()
    {
        // Lógica adicional usando aspectos
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.up * birdSpeed;
            
            // Logging del aspecto
            if (LoggingAspect.Instance != null)
            {
                LoggingAspect.LogPlayerAction("Bird Jump");
            }
        }
    }
    
    private void HandlePlayerCollision(GameObject other)
    {
        // La colisión se maneja en el aspecto, pero mantenemos la lógica original
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CounterPipes"))
        {
            Debug.Log("colisionando");
        }
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mantener la lógica original
        GameManager.instance.GameOver();
        
        // Notificar al aspecto de colisiones
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.Instance.RegisterCollision(gameObject, collision.gameObject);
        }
    }
    
    private void OnDestroy()
    {
        // Limpiar suscripciones
        if (InputAspect.Instance != null)
        {
            InputAspect.OnJumpInput -= HandleJumpInput;
        }
        
        if (CollisionAspect.Instance != null)
        {
            CollisionAspect.OnPlayerCollision -= HandlePlayerCollision;
        }
    }
}
