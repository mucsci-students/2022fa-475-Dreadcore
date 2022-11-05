using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthItem : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerDataT>().healthItem++;
            other.gameObject.GetComponent<playerDataT>().maxHealthItem++;
            Destroy(gameObject);
        }
    }
}

