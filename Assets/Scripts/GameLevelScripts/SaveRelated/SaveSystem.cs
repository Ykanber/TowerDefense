using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool isLoad;
    public static bool saveOnNextLevelScreen;

    public static void SavePlayer(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.codeway";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(gameManager);

        formatter.Serialize(stream,playerData);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/Player.codeway";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

}
