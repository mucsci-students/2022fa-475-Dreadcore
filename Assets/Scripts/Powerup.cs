using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private bool acquired = false;
    [SerializeField] private string name;
    [SerializeField] private PlayerMovement3 player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if triggered by player
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement3>();
            acquired = true;

            gameObject.SetActive(false);

            // Check name of powerup and unlock the corresponding ability
            if(name.Equals("Jump"))
            {  
                player.doubleJumpUnlocked = true;
            }
            if(name.Equals("Spider"))
            {
                player.spiderModeUnlocked = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
