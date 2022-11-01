using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
  Vector3 mousePos;
  private Camera mainCam;
  private Rigidbody2D rb;
  public float force;
  
  public float maxLifeTime;
  private float lifeTime = 0f;

  bool exploding = false;
  public float maxExplodeTime;
  private float explodeTime = 0f;

  Animator exploder;


  // Start is called before the first frame update
  void Start()
  {
    exploder = gameObject.GetComponent<Animator>();
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    rb = GetComponent<Rigidbody2D>();
    mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    Vector3 direction = mousePos - transform.position;
    Vector3 rotation = transform.position - mousePos;
    rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, rot);

    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
  }

  // Update is called once per frame
  void Update()
  {
    lifeTime += Time.deltaTime;
    
    if(exploding)
    {
      explodeTime += Time.deltaTime;
      if (explodeTime >= maxExplodeTime)
        Destroy(gameObject);
    }
    
    if(lifeTime >= maxLifeTime)
      Explode();

    
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Ground")
    {
      Explode();
    }
  }

  void Explode()
  {
    rb.velocity = new Vector2(0,0);
    exploding = true;
    exploder.Play("Hit-3 Animation");
  }


    
    
  
}
