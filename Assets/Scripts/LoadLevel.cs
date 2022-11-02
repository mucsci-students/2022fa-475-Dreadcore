using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    // Variable used to tell Unity what scene to load if condition is met
    [SerializeField] private string loadScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Load the scene name that's stored in the loadScene variable
            SceneManager.LoadScene(loadScene);
        }
    }
}
