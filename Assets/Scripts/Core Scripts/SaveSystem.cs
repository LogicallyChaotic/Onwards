using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //serializes the data into a custom binary file, and then saves this onto the hard disk to be loaded again
    //when loaded it deserialises the custom binary file and takes that data out in the form of Savedata a struct with all the information needed
    //and saved
    public static void SaveInfo(int player,int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.info";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, level);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/player.info";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("NONe");
            return null;
        }
    }

}

