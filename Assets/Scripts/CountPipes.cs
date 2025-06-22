using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPipes : MonoBehaviour
{
    private void Start()
    {
        scorePointPipes = FindObjectOfType<ScorePointPipes>();
    }
    private ScorePointPipes scorePointPipes;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scorePointPipes.scoreUp(1f);
        }
    }
}
