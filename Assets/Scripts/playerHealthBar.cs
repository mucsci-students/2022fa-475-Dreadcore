using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour
{
    /*public playerDataT player;
    public Image fillImage;
    private Slider slider;
    // Update is called once per frame
    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    public void SetHealth(int health, int maxHealth)
    {
        /*Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponent<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }
    public void Update()
    {
        float fillValue = player.health / player.maxHealth;
        slider.value = fillValue;
    }*/
    //////////////////////////////////////////////////////////////
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    // Update is called once per frame

    public void SetHealth(int health, int maxHealth)
    {
        Slider.gameObject.SetActive(true);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }
    void Update()
    {
        //Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }






}
