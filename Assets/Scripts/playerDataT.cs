using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerDataT : MonoBehaviour
{
    [SerializeField] public int health, maxHealth = 10, damage, healthItem, maxHealthItem;
    private playerRespawn respPlayer;


    //health bar
    public playerHealthBar Healthbar;

    private void Awake()
    {
        respPlayer = GetComponent<playerRespawn>();
        
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Healthbar.SetHealth(health, maxHealth);
        health = maxHealth;
        Healthbar.SetHealth(health, maxHealth);

    }
  

    //keep track of when our player takes damage
    public void TakeDamage(int damageAmt)
    {
        Debug.Log("taking Damage from something");
        health -= damageAmt;
        Healthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            //others scripts Respawn for checkpoints etc.
            respPlayer.Respawn();
        }
    }
    public void Respawn()
    {
        health = maxHealth;
        Healthbar.SetHealth(health, maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("apparently running into the monster constantly.");
            TakeDamage(1);
        }
    }
    public void UseHealthItem()
    {
        if (healthItem > 0)
        {
            if (health >= maxHealth)
            {
                healthItem = healthItem;
            
            }
            else if(health <= maxHealth)
            {
                health += 25;
                if (health >= maxHealth)
                {
                    health = maxHealth;
                    Healthbar.SetHealth(health, maxHealth);
                }
                healthItem--;
                Healthbar.SetHealth(health, maxHealth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            UseHealthItem();
        }
    }
}
