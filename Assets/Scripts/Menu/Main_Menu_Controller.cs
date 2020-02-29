using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{
    void Awake()
    {
        string path = Application.persistentDataPath + "/settings.gft";
        //Setting_Menu.Sound = true;

        if (File.Exists(path))
        {
            Game_Data data = Load.Load_Settings();
            Setting_Menu.Sound = data.Sound;
            //Setting_Menu.Music = data.Music;
        }
        else
        {
            Setting_Menu.Sound = true;
        }
        Debug.Log(Setting_Menu.Sound);
    }
}
