using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretBullet : MonoBehaviour
{
    public GameObject player;
    public GameObject spider;
    private PlayerMovement3 plyrmove3;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private float rot;
    private Vector3 direction;
    private bool inSpider;
    private bool OldSpiderBool;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spider = GameObject.FindGameObjectWithTag("Spider");
        plyrmove3 = player.GetComponent<PlayerMovement3>();
        inSpider = plyrmove3.spiderMode;
        OldSpiderBool = inSpider;
        detectPlayer();
        //detectSpider();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!plyrmove3.spiderMode)
        {
        //direction to where the bullet goes to
        direction = player.transform.position - transform.position;
        //speed of the bullet
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot);

        }
        else
        {
            //direction to where the bullet goes to
            direction = spider.transform.position - transform.position;
            //speed of the bullet
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rot);
        }*/
        //if (plyrmove3.morphing)
        //{
            if(plyrmove3.spiderMode == true)
            {
                //detectSpider();
                inSpider = true;
            }
            else
            {
                inSpider = false;
                //detectPlayer();
            }
            if (OldSpiderBool != inSpider)
            {
                detectPlayer();
            }
            OldSpiderBool = inSpider;




        //}
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!plyrmove3.spiderMode) 
        { 
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<playerDataT>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Spider"))
            {
                player.GetComponent<playerDataT>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
            
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void detectPlayer()
    {
        if(!inSpider)
        {

            direction = player.transform.position - transform.position;
            Debug.Log("This is the area of the player" + direction);
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rot);
         }
         else
         {
             direction = spider.transform.position - transform.position;
            Debug.Log("This is the area of the spider" + direction);
        
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rot);
         }
    }


    /*void detectSpider()
    {
        direction = spider.transform.position - transform.position;
        Debug.Log("This is the area of the spider" + direction);
        
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot);
    }*/
}
