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
        GameObject pipeVariable = Instantiate(pipe, spawnpos, Quaternion.identity);

        Destroy(pipeVariable, 10f);
    }
}
