using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerDataT : MonoBehaviour
{
    [SerializeField] public int health, maxHealth = 10, damage;
    private playerRespawn respPlayer;

    private void Awake()
    {
        respPlayer = GetComponent<playerRespawn>();
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }


    //keep track of when our player takes damage
    public void TakeDamage(int damageAmt)
    {
        health -= damageAmt;
        if (health <= 0)
        {
            respPlayer.Respawn();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        /*if (collider.gameObject.Tag == "enemy")
        {
            
        }*/
    }
    public void Respawn()
    {
        health = maxHealth;
        
    }











    // Update is called once per frame
    void Update()
    {
        
    }
}
