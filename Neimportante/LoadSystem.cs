using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSystem : MonoBehaviour
{
    public GameObject button1;
/*    public GameObject button2;
    public GameObject button3;*/
    private void Awake()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save1" + ".save"))
            button1.SetActive(true);
        else
            button1.SetActive(false);
/*        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save2" + ".save"))
            button2.SetActive(true);
       else
            button2.SetActive(false);
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save3" + ".save"))
            button3.SetActive(true);
        else
            button3.SetActive(false);*/
    }
    public void Save1Button()
    {
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
/*    public void Save2Button()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save2" + ".save"))
        {
        SaveManager.instance.activeSave.saveName = "save2";
        if(SaveManager.instance.activeSave.level == 0)
        LevelLoader.instance.LoadedLevel(1);
        else
        LevelLoader.instance.LoadedLevel(SaveManager.instance.activeSave.level);
        SaveManager.instance.Load();
        }
    }
    public void Save3Button()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/" + "save3" + ".save"))
        {
        SaveManager.instance.activeSave.saveName = "save3";
        if(SaveManager.instance.activeSave.level == 0)
        LevelLoader.instance.LoadedLevel(1);
        else
        LevelLoader.instance.LoadedLevel(SaveManager.instance.activeSave.level);
        SaveManager.instance.Load();
        }
    }*/
}
