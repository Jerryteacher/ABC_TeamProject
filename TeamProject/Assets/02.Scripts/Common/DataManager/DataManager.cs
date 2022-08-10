using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    private string dataPath;
    public void Initialize()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }
    public void Save(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        GameData data = new GameData();
        data.KillCount = gameData.KillCount;
        data.EnemyHp = gameData.EnemyHp;
        data.E_Damage = gameData.E_Damage;
        data.EnemyLevel = gameData.EnemyLevel;
        bf.Serialize(file, data);
        file.Close();
    }
    public GameData Load()
    {
        if(File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            GameData data = new GameData();
            return data;
        }
    }
}
