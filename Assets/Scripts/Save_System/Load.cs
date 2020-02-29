using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Load
{
    
    public static Game_Data Load_Settings()
    {
        string path = Application.persistentDataPath + "/settings.gft";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            Game_Data data = formatter.Deserialize(stream) as Game_Data;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
