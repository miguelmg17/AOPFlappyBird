using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject GameMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Play();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
}
