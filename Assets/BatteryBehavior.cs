using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBehavior : MonoBehaviour
{
    public playerDataT player;
    //public SpriteRenderer spriteRenderer;
    public Sprite Empty;
        public Sprite Full;
            public Sprite OneThree;
                public Sprite TwoThree;
                    public Sprite OneFour;
                        public Sprite TwoFour;
                            public Sprite ThreeFour;
                                public Sprite OneFifth;
                                    public Sprite TwoFifth;
                                        public Sprite ThreeFifth;
                                            public Sprite FourFifth;
    //public Sprite[] spriteArray;
    public Image MyImage;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player.maxHealthItem == 3)
        {
            if(player.healthItem == 0)
            {
                MyImage.sprite = Empty;
            }
            else if(player.healthItem == 1)
            {
                MyImage.sprite = OneThree;
            }
            else if(player.healthItem == 2)
            {
                MyImage.sprite = TwoThree;
            }
            else
            {
                 MyImage.sprite = Full;
            }
        }
        else if(player.maxHealthItem == 4)
        {
            if(player.healthItem == 0)
            {
                MyImage.sprite = Empty;
            }
            else if(player.healthItem == 1)
            {
                MyImage.sprite = OneFour;
            }
            else if(player.healthItem == 2)
            {
                MyImage.sprite = TwoFour;
            }
            else if(player.healthItem == 3)
            {
                MyImage.sprite = ThreeFour;
            }
            else
            {
                 MyImage.sprite = Full;
            }
        }
          else if(player.maxHealthItem == 5)
        {
            if(player.healthItem == 0)
            {
                MyImage.sprite = Empty;
            }
            else if(player.healthItem == 1)
            {
                MyImage.sprite = OneFifth;
            }
            else if(player.healthItem == 2)
            {
                MyImage.sprite = TwoFifth;
            }
            else if(player.healthItem == 3)
            {
                MyImage.sprite = ThreeFifth;
            }
             else if(player.healthItem == 4)
            {
                MyImage.sprite = FourFifth;
            }
            else
            {
                 MyImage.sprite = Full;
            }
        }

    }
}