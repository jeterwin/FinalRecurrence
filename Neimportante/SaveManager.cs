using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;

    private void Awake()
    {
        instance = this;

        activeSave.saveName = "save1";
        Load();
    }
    public void Update()
    {
                if(Input.GetKeyDown(KeyCode.K))
            DeleteSaveData1();
    }
    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            Debug.Log("Este");
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);

            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;
        }

    }

    public void DeleteSaveData1()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + "save1" + ".save"))
        {
            File.Delete(dataPath + "/" + "save1" + ".save");
        }
    }
    public void DeleteSaveData2()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + "save2" + ".save"))
        {
            File.Delete(dataPath + "/" + "save2" + ".save");
        }
    }
    public void DeleteSaveData3()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + "save3" + ".save"))
        {
            File.Delete(dataPath + "/" + "save3" + ".save");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public int level;
    public int chapter;

    //Trecut de primu cutscene
    public bool passedStory;

    public Vector3 respawnPosition;

    //Cate checkpointuri trebe
    public bool checkpoint1;
    public bool checkpoint2;
    public bool checkpoint3;
    public bool checkpoint4;
    public bool checkpoint5;
    public bool checkpoint6;

    //Cate baterii trebe
    public bool battery1;
    public bool battery2;
    public bool battery3;
    public bool battery4;
    public bool battery5;

    public bool medicine1;
    public bool medicine2;
    public bool medicine3;
    public bool medicine4;
    public bool medicine5;

    public bool hasFlashlight;

    public bool canOpenInventory;
    //Cate note trebe
    public bool note1;
    public bool note2;
    public bool note3;
    public bool note4;
    public bool note5;
    public bool note6;
    public bool note7;
    public bool note8;
    public bool note9;
    public bool note10;

    public bool placedFuse;

    public int batteries;
    public int medicines;
    public string currentObjective;

    public bool jumpscare1;
    public bool jumpscare2;
    public bool jumpscare3;
    public bool jumpscare4;
    public bool jumpscare5;
    //Objectivele incep sa fie numarate din level 2
    public bool objective1;
    public bool objective2;
    public bool objective3;
    public bool objective4;
    public bool objective5;

    public bool monologue1;
    public bool monologue2;
    public bool monologue3;
    public bool monologue4;
    public bool monologue5;
    public bool monologue6;
    public bool monologue7;
    public bool monologue8;
    public bool monologue9;
    public bool monologue10;
}