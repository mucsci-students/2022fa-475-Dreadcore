using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    [SerializeField] public bool isActive = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    public void ChangeSprite()
    {
        isActive = !isActive;
        if(isActive)
            spriteRenderer.sprite = activeSprite;
        else
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }
}
