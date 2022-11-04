using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] public bool isActive = false;
    [SerializeField] private Animator animator;
    [SerializeField] private PortalButton portalButton;
    [SerializeField] private LoadLevel nextLevel;
    // Update is called once per frame
    void Update()
    {
        // Set portal status to status of portal button
        isActive = portalButton.isActive;
        // Sets boolean value for portal animation to status of portal button
        animator.SetBool("isActive", portalButton.isActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isActive && other.CompareTag("Player"))
        {
            // TODO 
            SceneManager.LoadScene("TwistedCave");
        }
    }
}
