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
  private float speed = 4f;
  
  private bool isFacingRight = true;
  private bool playerOnGround = false;

  [SerializeField] public Rigidbody2D rb;
  [SerializeField] public Transform groundCheck;
  [SerializeField] public LayerMask groundLayer;

  private Animator animator;
  private string currentState;
  const string WALK = "Spider_Mode_Walk";
  const string IDLE = "Spider_Mode_Idle";
  const string FALL = "Spider_Mode_Fall";
  //const string FALL;
  //const string RETURN_TO_TORSO;
  //private Vector3 mousePos;
  
  // Start is called before the first frame update
  void Start()
  {
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    animator = gameObject.GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if(controllerEnabled)
    {
      mainCam.transform.position = cameraTransform.position;
   
      
      horizontal = Input.GetAxisRaw("Horizontal");
      playerOnGround = IsGrounded();

      if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        Flip();

      if(Input.GetKeyDown(KeyCode.F))
      {
        controllerEnabled = false;
        torso.torsoRecall();
      }
    }
  }

  void FixedUpdate()
  {
    float travelSpeed = Mathf.Abs(horizontal);
    if(controllerEnabled)
    {
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

      //Debug.Log(rb.velocity.y);

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
      Flip();
    
    controllerEnabled = true;
    mainCam.transform.position = cameraTransform.position;
  }
  
  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  void Flip()
  {
    isFacingRight = !isFacingRight;
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

}
