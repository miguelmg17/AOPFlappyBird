using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAspect : MonoBehaviour
{
    public static CollisionAspect Instance;
    
    // Eventos para diferentes tipos de colisión
    public static System.Action<GameObject, GameObject> OnCollisionDetected;
    public static System.Action<GameObject> OnPlayerCollision;
    public static System.Action<GameObject> OnPipeCollision;
    public static System.Action<GameObject> OnScoreTrigger;
    
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
    
    public void RegisterCollision(GameObject obj1, GameObject obj2)
    {
        OnCollisionDetected?.Invoke(obj1, obj2);
        
        // Determinar el tipo de colisión basado en tags
        if (obj1.CompareTag("Player") || obj2.CompareTag("Player"))
        {
            GameObject player = obj1.CompareTag("Player") ? obj1 : obj2;
            GameObject other = obj1.CompareTag("Player") ? obj2 : obj1;
            
            OnPlayerCollision?.Invoke(other);
        }
        
        if (obj1.CompareTag("CounterPipes") || obj2.CompareTag("CounterPipes"))
        {
            GameObject counter = obj1.CompareTag("CounterPipes") ? obj1 : obj2;
            OnScoreTrigger?.Invoke(counter);
        }
    }
    
    public void RegisterPipeCollision(GameObject pipe)
    {
        OnPipeCollision?.Invoke(pipe);
    }
    
    public void RegisterScoreTrigger(GameObject trigger)
    {
        OnScoreTrigger?.Invoke(trigger);
    }
} 