//using System.Net.Security;
//using System.Data;
//using System.Numerics;
//using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
  private float horizontal;
  private float speed = 8f;
  public float jumpingPower;
  private bool isFacingRight = true;
  public AudioSource audioSource;
  public AudioClip shootingClip;
  
  private bool playerOnGround = false;
  private bool startPose = true;

  [SerializeField] public Rigidbody2D rb;
  [SerializeField] public Transform groundCheck;
  [SerializeField] public LayerMask groundLayer;

  private PlayerAnimations animator;
  
  private Camera mainCam;
  public Transform cameraTransform;
  private Vector3 mousePos;

  public Transform gunPoint;
  
  // Determines whether to use aimLUp or aimRUp
  private bool upRight = true;

  public Transform aimRUp;
  public Transform aimRDiagonalUp;
  public Transform aimRight;
  public Transform aimRDiagonalDown;
  public Transform aimDown;
  public Transform aimLDiagonalDown;
  public Transform aimLeft;
  public Transform aimLDiagonalUp;
  public Transform aimLUp;

  private float rotZ = 0f;
  
  //Shooting
  public GameObject bullet;
  public GameObject dualBeam;
  public Transform bulletTransform;
  private bool canFire;
  private float timer = 0f;
  [SerializeField] private float timeBetweenFiring;
  
  //Upgrades
  public bool doubleJumpUnlocked = false;
  public bool spiderModeUnlocked = false;
  public bool dualBeamUnlocked = false;
  
  //Double Jump
  private bool doubleJump = true;

  //Spider Mode
  public bool spiderMode = false;
  public bool controllerEnabled = true;
  public GameObject spiderObject;
  private SpiderMode spiderController;
  public Transform spiderTransform;

  public bool morphing = false;
  private bool returning = false;
  private float morphTime = 0f;
  [SerializeField] private float maxMorphTime;
  private bool keyCooldown = false;

  public bool spiderInRange = false;

  void Start()
  {
    animator = gameObject.GetComponent<PlayerAnimations>();
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    spiderController = spiderObject.GetComponent<SpiderMode>();
  }

  void Update()
  {
    if(controllerEnabled)
    {
      mainCam.transform.position = cameraTransform.position;

      mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
      Vector3 rotation = mousePos - transform.position;
      rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
      gunPoint.rotation = Quaternion.Euler(0, 0, rotZ);
      

      horizontal = Input.GetAxisRaw("Horizontal");
      playerOnGround = IsGrounded();
      
      if(Mathf.Abs(horizontal) > 0.1f)
        startPose = false;
      
      if(horizontal > 0.1f)
      {
        isFacingRight = true;
        upRight = true;
      }
      else if (horizontal < -0.1f)
      {
        isFacingRight = false;
        upRight = false;
      }

      animator.updateSpawnPose(startPose);
      
      if ((Input.GetButtonDown("Jump") && playerOnGround) || (Input.GetButtonDown("Jump") && doubleJump && doubleJumpUnlocked))
      {
        if(!playerOnGround)
        {
          doubleJump = false;
          rb.velocity = new Vector2(rb.velocity.x, jumpingPower + 4f);
        }
        else
        {
          rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        
        startPose = false;
      }

      if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
      
      if(playerOnGround)
        doubleJump = true;

      if (!canFire)
      {
        timer += Time.deltaTime;
        if(timer > timeBetweenFiring)
        {
          canFire = true;
          timer = 0f;
        }
      }

      if(Input.GetMouseButton(0) && canFire)
      {
        canFire = false;
        startPose = false;
        if(dualBeamUnlocked)
          Instantiate(dualBeam, bulletTransform.position, Quaternion.identity);
        else
          audioSource.PlayOneShot(shootingClip);
          Instantiate(bullet, bulletTransform.position, Quaternion.identity);
      }

      //Spider Mode
      if(Input.GetKeyDown(KeyCode.F) && playerOnGround && !keyCooldown && spiderModeUnlocked)
      {
        morphing = true;
        spiderMode = true;
        animator.spiderMorph();
        controllerEnabled = false;
        keyCooldown = true;
        horizontal = 0f;
      }

      if(Input.GetKeyUp(KeyCode.F))
      {
        keyCooldown = false;
      }
    }
    
    else if (morphing)
    {
      morphTime += Time.deltaTime;

      if(morphTime >= maxMorphTime)
      {
        morphing = false;
        morphTime = 0f;
        spiderObject.transform.position = spiderTransform.position;
        spiderController.Activate();
      }
    }
  }

  private void FixedUpdate()
  {
    if (controllerEnabled)
    {
      animator.updateAnimData(Mathf.Abs(horizontal), playerOnGround, isFacingRight);
      updateGunpointPosition();
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    else
      rb.velocity = new Vector2(0,0);
  }

  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  public void torsoRecall()
  {
    animator.spiderReturn();
    mainCam.transform.position = cameraTransform.position;
    controllerEnabled = true;
    spiderMode = false;
  }

  public bool getPlayerDirection()
  {
    return isFacingRight;
  }

  void updateGunpointPosition()
  {
    
    if (rotZ <= 112.5f && rotZ > 67.5f)
    {
      animator.AimUp(upRight);
      if(upRight)
      {
        gunPoint.position = aimRUp.position;
      }      
      else
      {
        gunPoint.position = aimLUp.position;
      }
    }
    else if (rotZ <= 67.5f && rotZ > 22.5f)
    {
      animator.AimRDiaUp();
      gunPoint.position = aimRDiagonalUp.position;
      upRight = true;
    }
    else if (rotZ <= 22.5f && rotZ > -22.5f)
    {
      animator.AimRight();
      gunPoint.position = aimRight.position;
    }
    else if (rotZ <= -22.5f && rotZ > -67.5f)
    { 
      animator.AimRDiaDown();
      gunPoint.position = aimRDiagonalDown.position;
    }
    else if (rotZ <= -67.5f && rotZ > -112.5f)
    {
      animator.AimDown();
      gunPoint.position = aimDown.position;
    }
    else if (rotZ <= -112.5f && rotZ > -157.5f)
    {
      animator.AimLDiaDown();
      gunPoint.position = aimLDiagonalDown.position;
    }
    else if (rotZ <= -157.5f || rotZ > 157.5f)
    {
      animator.AimLeft();
      gunPoint.position = aimLeft.position;
    }
    else if (rotZ <= 157.5f && rotZ > 112.5f)
    {
      animator.AimLDiaUp();
      gunPoint.position = aimLDiagonalUp.position;
      upRight = false;
    } 
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