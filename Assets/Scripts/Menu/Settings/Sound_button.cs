using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_button : MonoBehaviour
{
    private Image img;

    public Animation Anim;
    public static bool Active_Sound; //работает

    public Image Color;
    public GameObject Img_Pos;

    //меняй!
    Color[] Colors = new Color[2] { new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 0.5f) };
    Vector2[] Pos = new Vector2[2] {new Vector2(1.08f, 1.47f), new Vector2(2.2f, 1.47f) };
    //меняю
    [SerializeField] Sprite[] Sprites;

    void Start()
    {
        img = GetComponent<Image>();
    }

    public void Off_On_Sound()
    {
        if(Active_Sound)
        {
            Active_Sound = false;
            Anim.Play("SB_Off");
            img.sprite = Sprites[1];
        }
        else
        {
            Active_Sound = true;
            Anim.Play("SB_On");
            img.sprite = Sprites[0];
        }
    }
}
