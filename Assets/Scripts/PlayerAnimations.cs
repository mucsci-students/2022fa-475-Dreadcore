using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

  private Animator animate;
  
  private bool startPose = true;
  private bool playerOnGround = false;
  private bool isFacingRight = true;
  private float speed = 0f;
  
  //determines if the legs should turn backwards if the aim is backwards.
  bool turnedLeft = false;
  bool turnedRight = false;

  //allows the turning animations to play if the player is aiming backwards and is idle on the ground.
  bool playing = false;

  //Animation states
  private string currentState;
  const string IDLE_AIM_DOWN = "Player_Idle_Aim_Down";

  //Right
  const string SPIDER_MORPH_R = "Player_Spider_Morph_R";
  
  const string RUN_R_AIM_UP = "Player_Run_R_Aim_Up";
  const string JUMP_R_AIM_UP = "Player_Jump_R_Aim_Up";
  const string IDLE_AIM_R_UP = "Player_Idle_Aim_R_Up";
  
  const string RUN_R_AIM_R_DIA_UP = "Player_Run_R_Aim_R_Dia_Up";
  const string JUMP_R_AIM_R_DIA_UP = "Player_Jump_R_Aim_R_Dia_Up";
  const string IDLE_AIM_R_DIA_UP = "Player_Idle_Aim_R_Dia_Up";

  const string RUN_R_AIM_R = "Player_Run_R_Aim_R";
  const string JUMP_R_AIM_R = "Player_Jump_R_Aim_R";
  const string IDLE_AIM_R = "Player_Idle_Aim_R";

  const string RUN_R_AIM_R_DIA_DOWN = "Player_Run_R_Aim_R_Dia_Down";
  const string JUMP_R_AIM_R_DIA_DOWN = "Player_Jump_R_Aim_R_Dia_Down";
  const string IDLE_AIM_R_DIA_DOWN = "Player_Idle_Aim_R_Dia_Down";
  
  const string RUN_R_AIM_DOWN = "Player_Run_R_Aim_Down";
  const string JUMP_R_AIM_DOWN = "Player_Jump_R_Aim_Down";
  //Idle down is direction neutral
  
  const string RUN_R_AIM_L_DIA_DOWN = "Player_Run_R_Aim_L_Dia_Down";
  const string JUMP_R_AIM_L_DIA_DOWN = "Player_Jump_R_Aim_L_Dia_Down";
  const string IDLE_R_TURN_L_DIA_DOWN = "Player_Idle_R_Turn_L_Dia_Down";

  const string RUN_R_AIM_L = "Player_Run_R_Aim_L";
  const string JUMP_R_AIM_L = "Player_Jump_R_Aim_L";
  const string IDLE_R_TURN_L = "Player_Idle_R_Turn_L";
  
  const string RUN_R_AIM_L_DIA_UP = "Player_Run_R_Aim_L_Dia_Up";
  const string JUMP_R_AIM_L_DIA_UP = "Player_Jump_R_Aim_L_Dia_Up";
  const string IDLE_R_TURN_L_DIA_UP = "Player_Idle_R_Turn_L_Dia_Up";

///////////////////
  
  //Left
  const string SPIDER_MORPH_L = "Player_Spider_Morph_L";

  const string RUN_L_AIM_UP = "Player_Run_L_Aim_Up";
  const string JUMP_L_AIM_UP = "Player_Jump_L_Aim_Up";
  const string IDLE_AIM_L_UP = "Player_Idle_Aim_L_Up";

  const string RUN_L_AIM_L_DIA_UP = "Player_Run_L_Aim_L_Dia_Up";
  const string JUMP_L_AIM_L_DIA_UP = "Player_Jump_L_Aim_L_Dia_Up";
  const string IDLE_AIM_L_DIA_UP = "Player_Idle_Aim_L_Dia_Up";

  const string RUN_L_AIM_L = "Player_Run_L_Aim_L";
  const string JUMP_L_AIM_L = "Player_Jump_L_Aim_L";
  const string IDLE_AIM_L = "Player_Idle_Aim_L";

  const string RUN_L_AIM_L_DIA_DOWN = "Player_Run_L_Aim_L_Dia_Down";
  const string JUMP_L_AIM_L_DIA_DOWN = "Player_Jump_L_Aim_L_Dia_Down";
  const string IDLE_AIM_L_DIA_DOWN = "Player_Idle_Aim_L_Dia_Down";
  
  const string RUN_L_AIM_DOWN = "Player_Run_L_Aim_Down";
  const string JUMP_L_AIM_DOWN = "Player_Jump_L_Aim_Down";
  //Idle down is direction neutral
  
  const string RUN_L_AIM_R_DIA_DOWN = "Player_Run_L_Aim_R_Dia_Down";
  const string JUMP_L_AIM_R_DIA_DOWN = "Player_Jump_L_Aim_R_Dia_Down";
  const string IDLE_L_TURN_R_DIA_DOWN = "Player_Idle_L_Turn_R_Dia_Down";

  const string RUN_L_AIM_R = "Player_Run_L_Aim_R";
  const string JUMP_L_AIM_R = "Player_Jump_L_Aim_R";
  const string IDLE_L_TURN_R = "Player_Idle_L_Turn_R";
  
  const string RUN_L_AIM_R_DIA_UP = "Player_Run_L_Aim_R_Dia_Up";
  const string JUMP_L_AIM_R_DIA_UP = "Player_Jump_L_Aim_R_Dia_Up";
  const string IDLE_L_TURN_R_DIA_UP = "Player_Idle_L_Turn_R_Dia_Up";


  // Start is called before the first frame update
  void Start()
  {
    animate = gameObject.GetComponent<Animator>();
  }

  /*void Update()
  {
    Debug.Log((runRightAimRight["Player_Run_R_Aim_R"].time).ToString);
  }*/

  public void updateSpawnPose(bool newStartPose)
  {
    startPose = newStartPose;
    animate.SetBool("SpawnPose", startPose);
  }

  public void updateAnimData(float newSpeed, bool newGroundStatus, bool newDirection)
  {
    speed = newSpeed;
    playerOnGround = newGroundStatus;
    isFacingRight = newDirection;
  }
  
  /*
  public void playAnimations()
  {
    
    if (startPose) return;
    
    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_L);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_L);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_L_AIM_L);
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_R);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_R);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_R_AIM_R);
    }
  }

*/

  public void AimUp(bool upRight)
  {
    
    if (startPose) return;
    
    playing = false;
    turnedLeft = true;
    turnedRight = true;

    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_UP);

      else if(upRight)
      {
        if(speed < 0.1f && playerOnGround)
          ChangeAnimationState(IDLE_AIM_R_UP);
        
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_R_AIM_UP);
      }
      
      else if(!upRight)
      {
        if(speed < 0.1f && playerOnGround)
          ChangeAnimationState(IDLE_AIM_L_UP);
        
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_L_AIM_UP);
      }
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_UP);
    
      else if(upRight)
      {
        if(speed < 0.1f && playerOnGround)
          ChangeAnimationState(IDLE_AIM_R_UP);
        
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_R_AIM_UP);
      }
      
      else if(!upRight)
      {
        if(speed < 0.1f && playerOnGround)
          ChangeAnimationState(IDLE_AIM_L_UP);
        
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_L_AIM_UP);
      }
    }
  }

  /******************/

  public void AimRDiaUp()
  {

    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedRight = false;
  
    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_L_AIM_R_DIA_UP);

      else if(!turnedRight)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_L_TURN_R_DIA_UP);
          turnedRight = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_L_AIM_R_DIA_UP);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_R_DIA_UP);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_R_AIM_R_DIA_UP);
          playing = false;
        }
      }
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_R_DIA_UP);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_R_DIA_UP);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_R_AIM_R_DIA_UP);
    }
  }

/******************/

  public void AimRight()
  {
    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedRight = false;
  
    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_L_AIM_R);

      else if(!turnedRight)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_L_TURN_R);
          turnedRight = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_L_AIM_R);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_R);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_R_AIM_R);
          playing = false;
        }
      }
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_R);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_R);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_R_AIM_R);
    }
  }

/******************/

  public void AimRDiaDown()
  {

    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedRight = false;
  
    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_L_AIM_R_DIA_DOWN);

      else if(!turnedRight)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_L_TURN_R_DIA_DOWN);
          turnedRight = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_L_AIM_R_DIA_DOWN);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_R_DIA_DOWN);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_R_AIM_R_DIA_DOWN);
          playing = false;
        }
      }
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_R_DIA_DOWN);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_R_DIA_DOWN);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_R_AIM_R_DIA_DOWN);
    }
  }

/******************/

  public void AimDown()
  {
    playing = false;
    turnedLeft = true;
    turnedRight = true;

    if (startPose) return;
    
    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_DOWN);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_DOWN);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_L_AIM_DOWN);
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_R_AIM_DOWN);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_DOWN);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_R_AIM_DOWN);
    }
  }

/******************/

  public void AimLDiaDown()
  {
    
    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedLeft = false;

    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_L_DIA_DOWN);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_L_DIA_DOWN);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_L_AIM_L_DIA_DOWN);
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_R_AIM_L_DIA_DOWN);

      else if(!turnedLeft)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_R_TURN_L_DIA_DOWN);
          turnedLeft = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_R_AIM_L_DIA_DOWN);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_L_DIA_DOWN);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_L_AIM_L_DIA_DOWN);
          playing = false;
        }
      }
    }
  }

/******************/

  public void AimLeft()
  {
    
    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedLeft = false;

    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_L);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_L);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_L_AIM_L);
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_R_AIM_L);

      else if(!turnedLeft)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_R_TURN_L);
          turnedLeft = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_R_AIM_L);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_L);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_L_AIM_L);
          playing = false;
        }
      }
    }
  }

/******************/

  public void AimLDiaUp()
  {
    
    if (startPose) return;
    
    if (speed > 0.1f) 
      turnedLeft = false;

    if(!isFacingRight)
    {
      if(speed > 0.1f && playerOnGround)
        ChangeAnimationState(RUN_L_AIM_L_DIA_UP);
    
      else if(speed < 0.1f && playerOnGround)
        ChangeAnimationState(IDLE_AIM_L_DIA_UP);
      
      else if(!playerOnGround)
        ChangeAnimationState(JUMP_L_AIM_L_DIA_UP);
    }
    else
    {
      if(speed > 0.1f && playerOnGround)
          ChangeAnimationState(RUN_R_AIM_L_DIA_UP);

      else if(!turnedLeft)
      {
        if(speed < 0.1f && playerOnGround)
        {
          ChangeAnimationState(IDLE_R_TURN_L_DIA_UP);
          turnedLeft = true;
          playing = true;
        }
        else if(!playerOnGround)
          ChangeAnimationState(JUMP_R_AIM_L_DIA_UP);
      }
      else
      {
        if(speed < 0.1f && playerOnGround && !playing)
        {
          ChangeAnimationState(IDLE_AIM_L_DIA_UP);
        }
        else if(!playerOnGround)
        {
          ChangeAnimationState(JUMP_L_AIM_L_DIA_UP);
          playing = false;
        }
      }
    }
  }

  //////////////////////////

  public void spiderMorph()
  {
    if(!isFacingRight)
    {
      ChangeAnimationState(SPIDER_MORPH_L);
    }
    else
    {
      ChangeAnimationState(SPIDER_MORPH_R);
    }
  }

  void ChangeAnimationState(string newState)
  {
    if (currentState == newState) return;

    animate.Play(newState);
    currentState = newState;
  }

}

/* 

DIRECTIONAL AIMING THRESHOLDS

Up          : x <= 112.5  x > 67.5

R Diag Up   : x <= 67.5   x > 22.5

Right       : x <= 22.5   x > -22.5

R Diag Down : x <= -22.5  x > -67.5

Down        : x <= -67.5  x > -112.5

L Diag Down : x <= -112.5 x > -157.5

Left        : x <= -157.5 x > 157.5

L Diag Up   : x <= 157.5  x > 112.5


*/