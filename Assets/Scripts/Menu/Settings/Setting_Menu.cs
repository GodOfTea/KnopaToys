using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Setting_Menu : MonoBehaviour
{
    //---ПЕРЕМЕННЫЕ---
    //---ЗВУК и МУЗЫКА---
    public static bool         Sound;
    public static bool         Music;
    public Vector3             Audio_Button_Pos;
    public int                 Img_Index;
    [SerializeField] Animation Audio_Anim;
    [SerializeField] Sprite[]  Audio_Sprites;
    [SerializeField] Image     Sound_Img;
    [SerializeField] Image     Music_Img;
    //---ЗВУК и МУЗЫКА---

    //---ЯЗЫК---

    //---ЯЗЫК---
    //---ПЕРЕМЕННЫЕ---
    
    void Start()
    {
        //string path = Application.persistentDataPath + "/settings.gft";
        //Debug.Log(Sound);
        //if (File.Exists(path))
        //{
        //    Load_Settings();
        //}
        //Debug.Log(Sound);
    }
    public void SetSound ()
    {
        Sound = !Sound;

        if(Sound)
        {
            Audio_Anim.Play("Audio_On");
            Img_Index = 0;
            Sound_Img.sprite = Audio_Sprites[Img_Index];
        }
        else
        {
            Audio_Anim.Play("Audio_Off");
            Img_Index = 1;
            Sound_Img.sprite = Audio_Sprites[Img_Index];
        }

        Audio_Button_Pos = -Sound_Img.transform.GetChild(0).localPosition;
        Save_Settings();
        Debug.Log(Audio_Button_Pos);
    }

    public void Save_Settings()
    {
        Debug.Log("Save");
        Save.Save_Settings(this);
    }
    public void Load_Settings()
    {
        Debug.Log("Load");
        Game_Data data = Load.Load_Settings();

        Sound = data.Sound;
        Music = data.Music;
        Sound_Img.sprite = Audio_Sprites[data.Img_Index];

        Vector3 pos = new Vector3();
        pos.x = data.Position[0];
        pos.y = data.Position[1];
        pos.z = data.Position[2];
        Audio_Button_Pos = pos;
    }

    //---SAVE---
    //Что уходит в SAVE?
    //1. bool Sound
    //2. Image Sound_Img
    //3. Vector3 Audio_Button_Pos
    //---SAVE---
}
