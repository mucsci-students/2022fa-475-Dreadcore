using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public GameObject spider;
    private float distance;
    private PlayerMovement3 plyrmove3;
    public Transform bulletPos;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spider = GameObject.FindGameObjectWithTag("Spider");
        plyrmove3 = player.GetComponent<PlayerMovement3>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!plyrmove3.spiderMode)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
        }
        else
        {
            distance = Vector2.Distance(transform.position, spider.transform.position);
        }
        //float distance2 = Vector2.Distance(transform.position, spider.transform.position);
        //Debug.Log(distance);
        if (!plyrmove3.spiderMode)
        {
            if (distance < 4)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;
                    shoot();
                }
            }
        }
        else
        {
            if (distance < 4)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;
                    shoot();
                }
            }
         }
        
    }
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
