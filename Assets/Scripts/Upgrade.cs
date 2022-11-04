using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
  public PlayerMovement3 player;

  public bool doubleJump = false;
  public bool spiderMode = false;
  public bool dualBeam = false;

  // Update is called once per frame
  void OnTriggerEnter(Collider other)
  {
    Debug.Log("Collision detected!");
    if(other.gameObject.tag == "Player")
    {
      if (doubleJump)
        player.doubleJumpUnlocked = true;
      else if (spiderMode)
        player.spiderModeUnlocked = true;
      else if (dualBeam)
        player.dualBeamUnlocked = true;
      
      //UI code goes here
      Destroy(gameObject);
    }
  }
}
