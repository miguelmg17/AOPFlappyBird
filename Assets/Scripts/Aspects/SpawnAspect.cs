using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAspect : MonoBehaviour
{
    public static SpawnAspect Instance;
    
    // Eventos para spawn y destrucción
    public static System.Action<GameObject> OnObjectSpawned;
    public static System.Action<GameObject> OnObjectDestroyed;
    public static System.Action<GameObject> OnPipeSpawned;
    
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
    
    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation = default)
    {
        if (prefab == null) return null;
        
        GameObject spawnedObject = Instantiate(prefab, position, rotation);
        OnObjectSpawned?.Invoke(spawnedObject);
        
        // Evento específico para tuberías
        if (spawnedObject.name.Contains("Pipe") || spawnedObject.CompareTag("Pipe"))
        {
            OnPipeSpawned?.Invoke(spawnedObject);
        }
        
        return spawnedObject;
    }
    
    public void DestroyObject(GameObject obj, float delay = 0f)
    {
        if (obj == null) return;
        
        if (delay <= 0f)
        {
            OnObjectDestroyed?.Invoke(obj);
            Destroy(obj);
        }
        else
        {
            StartCoroutine(DestroyObjectDelayed(obj, delay));
        }
    }
    
    private IEnumerator DestroyObjectDelayed(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (obj != null)
        {
            OnObjectDestroyed?.Invoke(obj);
            Destroy(obj);
        }
    }
    
    public GameObject SpawnPipe(GameObject pipePrefab, Vector3 position)
    {
        return SpawnObject(pipePrefab, position);
    }
} 