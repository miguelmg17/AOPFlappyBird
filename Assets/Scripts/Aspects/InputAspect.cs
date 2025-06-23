using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputAspect : MonoBehaviour
{
    public static InputAspect Instance;
    
    // Eventos para diferentes tipos de input
    public static System.Action OnJumpInput;
    public static System.Action OnMenuInput;
    public static System.Action OnRestartInput;
    public static System.Action OnExitInput;
    
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
    
    private void Update()
    {
        ProcessInput();
    }
    
    private void ProcessInput()
    {
        // Input para salto (W, UpArrow, clic izquierdo)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnJumpInput?.Invoke();
        }
        
        // Input para menú (Enter)
        if (Input.GetKey(KeyCode.Return))
        {
            OnMenuInput?.Invoke();
        }
        
        // Input para reiniciar (Espacio)
        if (Input.GetKey(KeyCode.Space))
        {
            OnRestartInput?.Invoke();
        }
        
        // Input para salir (ESC)
        if (Input.GetKey(KeyCode.Escape))
        {
            OnExitInput?.Invoke();
        }
    }
} 