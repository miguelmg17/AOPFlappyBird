using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject pipe;
    private ScorePointPipes scorePointPipes;
    private float timer;

    private void Start()
    {
        SpawnPipe();
        
        // Suscribirse a eventos de aspectos
        if (SpawnAspect.Instance != null)
        {
            SpawnAspect.OnPipeSpawned += HandlePipeSpawned;
        }
    }

    private void Update()
    {
        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnpos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        
        // Usar el aspecto de spawn si está disponible
        if (SpawnAspect.Instance != null)
        {
            GameObject pipeVariable = SpawnAspect.Instance.SpawnPipe(pipe, spawnpos);
            if (pipeVariable != null)
            {
                SpawnAspect.Instance.DestroyObject(pipeVariable, 10f);
            }
        }
        else
        {
            // Mantener la lógica original como fallback
            GameObject pipeVariable = Instantiate(pipe, spawnpos, Quaternion.identity);
            Destroy(pipeVariable, 10f);
        }
    }
    
    private void HandlePipeSpawned(GameObject spawnedPipe)
    {
        // Lógica adicional cuando se spawnea una tubería
        if (LoggingAspect.Instance != null)
        {
            LoggingAspect.LogSpawn("Pipe", spawnedPipe.transform.position);
        }
    }
    
    private void OnDestroy()
    {
        // Limpiar suscripciones
        if (SpawnAspect.Instance != null)
        {
            SpawnAspect.OnPipeSpawned -= HandlePipeSpawned;
        }
    }
}
