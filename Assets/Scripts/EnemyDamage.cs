using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public playerDataT plyrData;
    public HealthBarBehavior Healthbar;
    public int health;
    public int maxHealth  = 5;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Healthbar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.gameObject.tag == "Player");
        {
            plyrData.TakeDamage(damage);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            AITakeDamage(2);
        }
    }



    public void AITakeDamage(int damage)
    {
        health = health - damage;
        Healthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

}
