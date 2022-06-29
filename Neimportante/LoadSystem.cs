using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSystem : MonoBehaviour
{
    public GameObject button1;
    private void Awake()
    {
        //If there is a saved file, activate the "Continue" button
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save1" + ".save"))
            button1.SetActive(true);
        else
            button1.SetActive(false);
    }
    public void Save1Button()
    {
        //If there is a saved file, set the file name and load it's data on button press
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save1" + ".save"))
        {
        SaveManager.instance.activeSave.saveName = "save1";
        if(SaveManager.instance.activeSave.level == 0)
        LevelLoader.instance.LoadedLevel(1);
        else
        LevelLoader.instance.LoadedLevel(SaveManager.instance.activeSave.level);
        SaveManager.instance.Load();
        }
    }
}
