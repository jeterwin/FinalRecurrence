using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SaveData activeSave;
    public Animator animatorBattery;
    public GameObject saveGame;
    bool hasPlayed = false;
    private void Start()
    {
        if(SaveManager.instance.hasLoaded)
        {
            if(SaveManager.instance.activeSave.checkpoint1 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag != null)
        {
            if(other.tag == "Player" && hasPlayed == false)
            {
                hasPlayed = true;
                GameManager.instance.respawnPoint = other.transform.position;
                SaveManager.instance.activeSave.respawnPosition = other.transform.position;
                SaveManager.instance.activeSave.currentObjective = GameManager.instance.activeSave.currentObjective;
                GameManager.instance.activeSave.checkpoint1 = true;
                SaveManager.instance.activeSave.checkpoint1 = true;
                SaveManager.instance.activeSave.batteries = Flashlight_PRO.instance.batteries;
                SaveManager.instance.activeSave.canOpenInventory = true;
                SaveManager.instance.activeSave.hasFlashlight = true;
                SaveManager.instance.activeSave.passedStory = true;
                SaveManager.instance.activeSave.objective1 = GameManager.instance.activeSave.objective1;
                SaveManager.instance.activeSave.objective2 = GameManager.instance.activeSave.objective2;
                SaveManager.instance.activeSave.objective3 = GameManager.instance.activeSave.objective3;
                SaveManager.instance.activeSave.objective4 = GameManager.instance.activeSave.objective4;
                SaveManager.instance.activeSave.objective5 = GameManager.instance.activeSave.objective5;
                SaveManager.instance.activeSave.level = 3;
                SaveManager.instance.activeSave.chapter = 1;
                SaveManager.instance.hasLoaded = true;
                batteryCount.instance.UpdateBatteries();
                animatorBattery.Play("BatteryCanvasInstant");
                //saveGame.Play("SavingGame");
                StartCoroutine(disable());
                SaveManager.instance.Save();
            }
        }

    }
    IEnumerator disable()
    {
        saveGame.SetActive(true);
        yield return new WaitForSeconds(4f);
        saveGame.SetActive(false);
        Destroy(this.gameObject);
        yield return null;
    }
}
