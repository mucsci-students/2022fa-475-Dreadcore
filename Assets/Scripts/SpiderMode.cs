using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMode : MonoBehaviour
{
  public bool controllerEnabled = false;
  private Camera mainCam;
  public Transform cameraTransform;
  public PlayerMovement3 torso;

  private float horizontal;
  private float vertical;

  private float speed = 4f;
  
  public bool isFacingRight = true; // Ground/Ceiling: spider faces right. Walls: On right wall = true, On left wall = false.
  public bool isFacingUp = true; // Walls: spider faces up. Ground/Ceiling: On ground = true, on ceiling = false.

  public bool playerOnGround = false;
  public bool sideways = false; //True if spider is on left or right wall.
  private bool hitWall = false;
  
  private float spiderRotation;
  
  //public float collisionRadius;

  private float tempGravity;

  [SerializeField] public Rigidbody2D rb;
  [SerializeField] public Transform groundCheck;
  [SerializeField] public Transform wallCheck;
  [SerializeField] public Transform wallCheck2;

  //[SerializeField] public Transform LWallCheck;
  
  [SerializeField] public LayerMask groundLayer;

  private Animator animator;
  private string currentState;
  const string WALK = "Spider_Mode_Walk";
  const string IDLE = "Spider_Mode_Idle";
  const string FALL = "Spider_Mode_Fall";
  const string RETURN_TO_TORSO = "Spider_Mode_Return";
  //private Vector3 mousePos;

  private float rotZ;
  public Transform c_clockwiseReference;
  public Transform clockwiseReference;

  public bool rotating = false;
  private float resetTimer = 0f;
  private float maxResetTimer = 0.2f;

  public bool lockRotation = false;
  public bool clockwise = false;

  //Return to torso
  bool returning = false;
  [SerializeField] private float returnSpeed = 10000f;
  public Transform Vault;
  float returnTimer = 0f;
  float maxReturnTimer = 3f;

  public bool test;

  // Start is called before the first frame update
  void Start()
  {
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    animator = gameObject.GetComponent<Animator>();
    spiderRotation = transform.rotation.z;
    tempGravity = rb.gravityScale;
  }

  // Update is called once per frame
  void Update()
  {
    
    spiderRotation = transform.rotation.z;
    
    //Debug.Log("Spider Rotation: " + (spiderRotation).ToString());
    //Debug.Log("Velocity X : " + (rb.velocity.x).ToString());
    //Debug.Log("Velocity Y : " + (rb.velocity.y).ToString());
    //Debug.Log("Rotation Z : " + (rotZ).ToString());
    
    if(controllerEnabled)
    {
      if (test)
        testReturn();
      
      mainCam.transform.position = cameraTransform.position;
   
      if(!sideways)
        horizontal = Input.GetAxisRaw("Horizontal");
      
      else
        vertical = Input.GetAxisRaw("Vertical");
  

      playerOnGround = IsGrounded();

      if(!sideways && (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f))
        FlipHorizontal();

      if(sideways && (isFacingUp && vertical < 0f || !isFacingUp && vertical > 0f))
        FlipVertical();
      
      checkForWalls();
      if(clockwise)
      {
        clockwise = false;
      }

      if(Input.GetKeyDown(KeyCode.F))
      {
        ReturnToTorso();
      }

      if(Input.GetKeyDown(KeyCode.LeftShift) && playerOnGround)
      {
        transform.position = c_clockwiseReference.position;
      }
      
      if((!playerOnGround && rotZ != 0f && !rotating))
      {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rotZ = 0;
        sideways = false;
        
        if(!isFacingUp)
        {
          FlipHorizontal();
          isFacingRight = !isFacingRight;
        }

        isFacingUp = true;
        rb.gravityScale = tempGravity;
      }

      if (rotating)
      {
        resetTimer += Time.deltaTime;
        if(resetTimer >= maxResetTimer)
        {
          rotating = false;
          lockRotation = false;
          resetTimer = 0f;
        }
      }

    }

    if(rotZ == 0)
      isFacingUp = true;

    if(returning)
    {
      mainCam.transform.position = cameraTransform.position;
      rb.gravityScale = 0f;
      transform.rotation = Quaternion.Euler(0, 0, 0);
      rotZ = 0;
      sideways = false;
    
      if(!isFacingUp)
      {
        FlipHorizontal();
        isFacingRight = !isFacingRight;
      }
      isFacingUp = true;
      
      ChangeAnimationState(RETURN_TO_TORSO);
      transform.position = Vector2.MoveTowards(transform.position, torso.spiderTransform.position, returnSpeed * Time.deltaTime);
      returnTimer += Time.deltaTime;

      if((torso.spiderTransform.position.x == transform.position.x && torso.spiderTransform.position.y == transform.position.y) || returnTimer >= maxReturnTimer)
      { 
        returnTimer = 0f;
        transform.position = Vault.transform.position;
        torso.torsoRecall();
        rb.gravityScale = tempGravity;
        returning = false;
        ChangeAnimationState(IDLE);
        if (!isFacingRight)
          FlipHorizontal();
      }
      
    }
    
  }




  void ReturnToTorso()
  {
    //if (torso.spiderInRange)
    //{
      controllerEnabled = false;
      returning = true;  
    //}
    //else
      //Debug.Log("Out of range"); // Replace with UI
  }




  void FixedUpdate()
  {
    float travelSpeed;
    if(!sideways)
      travelSpeed = Mathf.Abs(horizontal);
    else
      travelSpeed = Mathf.Abs(vertical);
    if(controllerEnabled)
    {
      float stickingForce = 0f;
      
      if(rotZ != 0)
      {
        stickingForce = 1f;
        if(sideways)
        {
          if (isFacingRight)
            rb.velocity = new Vector2(rb.velocity.x + stickingForce, vertical * speed);
          else
            rb.velocity = new Vector2(rb.velocity.x - stickingForce, vertical * speed);
        }
        else
        {
          if (!isFacingUp)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y + stickingForce);
          else
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
      }
      else
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

      if (travelSpeed > 0.1f && playerOnGround)
        ChangeAnimationState(WALK);
      else if ((travelSpeed < 0.1f && playerOnGround) || !playerOnGround && rb.velocity.y > -4f)
        ChangeAnimationState(IDLE);
      else if (!playerOnGround && rb.velocity.y < -4f)
        ChangeAnimationState(FALL);
    }
  }

  public void Activate()
  {
    if(!torso.getPlayerDirection())
      FlipHorizontal();
    
    controllerEnabled = true;
    mainCam.transform.position = cameraTransform.position;
  }
  
  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }


// -0.7071068
  void Rotate()
  {
    if (hitWall && !lockRotation)
    {
       
      float oldRot = rotZ;

      //lockRotation = clockwiseFix();
      clockwise = clockwiseFix();
      Vector3 rotation;      
      if (clockwise)
        rotation = clockwiseReference.position - transform.position;
      else  
        rotation = c_clockwiseReference.position - transform.position;
      
      rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, rotZ);
      
      
      hitWall = false;

      if (sideways)
      {
        isFacingRight = !isFacingRight;
        isFacingUp = !isFacingUp;
      }
      

      sideways = !sideways;

      if(rotZ != 0f)
        rb.gravityScale = 0f;
      else
      {
        rb.gravityScale = tempGravity;
        isFacingUp = true;
      }
      if(rotZ == 180f)
        isFacingUp = false;
      
      rotating = true;
      lockRotation = true;
    }
  }

  void checkForWalls()
  {
    hitWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer) && Physics2D.OverlapCircle(wallCheck2.position, 0.2f, groundLayer);
    //if((isFacingRight && !sideways) || (isFacingUp && sideways))
      Rotate();
  }

  bool clockwiseFix()
  {
    if(rotZ == 0f && !isFacingRight)
      rotZ = -90f;
    
    else if(rotZ == -90f && isFacingUp)
      rotZ = 180f;
    
    else if (rotZ == 180f && isFacingRight)
      rotZ = 90f;

    else if (rotZ == 90f && !isFacingUp)
      rotZ = 0f;
    else
      return false;
    
    return true;
  }

  void FlipHorizontal()
  {
    isFacingRight = !isFacingRight;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
  }

  void FlipVertical()
  {
    isFacingUp = !isFacingUp;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
  }

  void ChangeAnimationState(string newState)
  {
    if (currentState == newState) return;

    animator.Play(newState);
    currentState = newState;
  }

  void testReturn()
  {
    controllerEnabled = false;
    transform.position = Vault.transform.position;
  }

  
}
