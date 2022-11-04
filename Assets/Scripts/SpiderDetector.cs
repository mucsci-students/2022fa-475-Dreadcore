using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDetector : MonoBehaviour
{
  private GameObject playerObj;
  private PlayerMovement3 player;
  private GameObject spider;

  void Start()
  {
    playerObj = GameObject.Find("Player");
    spider = GameObject.Find("Spider Mode");
    player = playerObj.GetComponent<PlayerMovement3>();
  }

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
      player.spiderInRange = true;
  }

  void OnTriggerExit(Collider other)
  {
    if(other.gameObject.tag == "Player")
      player.spiderInRange = false;
  }

}
