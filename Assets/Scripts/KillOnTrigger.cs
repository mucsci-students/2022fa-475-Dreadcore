using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            // Exit the Run mode of unity if entered
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
