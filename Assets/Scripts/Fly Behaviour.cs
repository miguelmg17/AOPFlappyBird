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

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Mouse.current.leftButton.wasPressedThisFrame)
        {
            rb2d.velocity = Vector2.up * birdSpeed;
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
        GameManager.instance.GameOver();
    }
}
