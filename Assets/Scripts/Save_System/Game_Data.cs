using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game_Data
{
    public bool    Sound;
    public bool    Music;
    public int     Img_Index;
    public float[] Position;


    public Game_Data (Setting_Menu setting_menu)
    {
        Sound = Setting_Menu.Sound;
        Music = Setting_Menu.Music;
        Img_Index = setting_menu.Img_Index;

        Position = new float[3];
        Position[0] = setting_menu.Audio_Button_Pos.x;
        Position[1] = setting_menu.Audio_Button_Pos.y;
        Position[2] = setting_menu.Audio_Button_Pos.z;
    }
}
