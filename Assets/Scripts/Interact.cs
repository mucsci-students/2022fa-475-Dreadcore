using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    private PortalButton btn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            // TODO : activate the portal to next level when button is enabled
            btn = gameObject.GetComponent<PortalButton>();
            btn.ChangeSprite();
        }
    }
}
