using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save
{
    
    public static void Save_Settings(Setting_Menu setting_menu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.gft";

        FileStream stream = new FileStream(path, FileMode.Create);
        Game_Data data = new Game_Data(setting_menu);
        formatter.Serialize(stream, data);
        
        stream.Close();
    }
}
