using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    TextAsset dollList;
    Dictionary<string, bool> dollsCollected = new Dictionary<string, bool>();

    // Use this for initialization
    void Awake () {
        if (control == null)
        {
            // Load the master doll list and use it to populate dollsCollected
            dollList = (TextAsset)Resources.Load("dollList");
            string dollListStr = dollList.text;
            string[] dollListArr = dollListStr.Split(' ');
            foreach (string s in dollListArr)
            {
                dollsCollected.Add(s, false);
            }


            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
	}

    public void CollectDoll(string dollID)
    {
        if (!dollsCollected.ContainsKey(dollID))
        {
            Debug.Log("Doll not in master list.");
            return;
        }
        dollsCollected[dollID] = true;
        Save();
        PrintCollected();
    }

    public void Save()
    {
        //  Open a formatter and a file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        //  Store variables in simple class
        PlayerData data = new PlayerData();
        data.dollsCollected = dollsCollected;

        //  Serialize, store in file, close file
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            dollsCollected = data.dollsCollected;
        }
    }

    public void PrintCollected()
    {
        foreach (KeyValuePair<string, bool> d in dollsCollected)
        {
            Debug.Log(d.Key + " " + d.Value);
        }
    }
}

[Serializable]
class PlayerData
{
    public Dictionary<string, bool> dollsCollected = new Dictionary<string, bool>();
}
