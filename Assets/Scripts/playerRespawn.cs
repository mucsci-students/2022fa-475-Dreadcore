using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
   private Transform currentCheckpoint;
   private playerDataT playerHealth;
   private void Awake()
   {
       playerHealth = GetComponent<playerDataT>();
   }
   public void Respawn()
   {
       transform.position = currentCheckpoint.position;
       playerHealth.Respawn();
   }
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.transform.tag == "Checkpoint")
       {
           currentCheckpoint = collision.transform;
           collision.GetComponent<Collider2D>().enabled = false;
           collision.GetComponent<Animator>().SetTrigger("Appear");

       }
   }
}
