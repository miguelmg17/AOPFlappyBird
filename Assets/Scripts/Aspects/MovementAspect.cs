using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAspect : MonoBehaviour
{
    public static MovementAspect Instance;
    
    // Eventos para movimiento
    public static System.Action<GameObject, Vector3> OnObjectMoved;
    public static System.Action<GameObject, Vector3> OnVelocityChanged;
    
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
    
    public void MoveObject(GameObject obj, Vector3 direction, float speed)
    {
        if (obj == null) return;
        
        Vector3 movement = direction * speed * Time.deltaTime;
        obj.transform.position += movement;
        
        OnObjectMoved?.Invoke(obj, movement);
    }
    
    public void SetObjectVelocity(GameObject obj, Vector3 velocity)
    {
        if (obj == null) return;
        
        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = velocity;
            OnVelocityChanged?.Invoke(obj, velocity);
        }
    }
    
    public void MoveObjectTowards(GameObject obj, Vector3 target, float speed)
    {
        if (obj == null) return;
        
        Vector3 direction = (target - obj.transform.position).normalized;
        MoveObject(obj, direction, speed);
    }
    
    public void MoveObjectConstant(GameObject obj, Vector3 direction, float speed)
    {
        if (obj == null) return;
        
        obj.transform.position += direction * speed * Time.deltaTime;
        OnObjectMoved?.Invoke(obj, direction * speed * Time.deltaTime);
    }
    
    public void SetObjectPosition(GameObject obj, Vector3 position)
    {
        if (obj == null) return;
        
        Vector3 oldPosition = obj.transform.position;
        obj.transform.position = position;
        
        OnObjectMoved?.Invoke(obj, position - oldPosition);
    }
} 