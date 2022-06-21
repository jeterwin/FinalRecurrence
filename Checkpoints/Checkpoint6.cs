using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint6 : MonoBehaviour
{
    public SaveData activeSave;
    public GameObject saveGame;
    bool hasPlayed = false;
    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.checkpoint7 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.tag != null)
        {
            if (other.tag == "Player" && hasPlayed == false)
            {
                GameManager.instance.respawnPoint = other.transform.position;

                SaveManager.instance.activeSave.respawnPosition = other.transform.position;
                SaveManager.instance.activeSave.checkpoint7 = true;
                SaveManager.instance.activeSave.placedFuse = GameManager.instance.activeSave.placedFuse;
                SaveManager.instance.activeSave.currentObjective = GameManager.instance.activeSave.currentObjective;
                SaveManager.instance.activeSave.batteries = Flashlight_PRO.instance.batteries;
                //SaveManager.instance.activeSave.medicines = HealthSystem.instance.DrugsAmount;
                //Medicine
                SaveManager.instance.activeSave.medicine1 = GameManager.instance.activeSave.medicine1;
                SaveManager.instance.activeSave.medicine2 = GameManager.instance.activeSave.medicine2;
                SaveManager.instance.activeSave.medicine3 = GameManager.instance.activeSave.medicine3;
                SaveManager.instance.activeSave.medicine4 = GameManager.instance.activeSave.medicine4;
                SaveManager.instance.activeSave.medicine5 = GameManager.instance.activeSave.medicine5;
                //Notes
                SaveManager.instance.activeSave.note1 = GameManager.instance.activeSave.note1;
                SaveManager.instance.activeSave.note2 = GameManager.instance.activeSave.note2;
                SaveManager.instance.activeSave.note3 = GameManager.instance.activeSave.note3;
                SaveManager.instance.activeSave.note4 = GameManager.instance.activeSave.note4;
                SaveManager.instance.activeSave.note5 = GameManager.instance.activeSave.note5;
                SaveManager.instance.activeSave.note6 = GameManager.instance.activeSave.note6;
                SaveManager.instance.activeSave.note7 = GameManager.instance.activeSave.note7;
                SaveManager.instance.activeSave.note8 = GameManager.instance.activeSave.note8;
                SaveManager.instance.activeSave.note9 = GameManager.instance.activeSave.note9;
                SaveManager.instance.activeSave.note10 = GameManager.instance.activeSave.note10;
                SaveManager.instance.activeSave.note11 = GameManager.instance.activeSave.note11;
                SaveManager.instance.activeSave.note12 = GameManager.instance.activeSave.note12;
                SaveManager.instance.activeSave.note13 = GameManager.instance.activeSave.note13;
                SaveManager.instance.activeSave.note14 = GameManager.instance.activeSave.note14;
                //Jumpscares
                SaveManager.instance.activeSave.jumpscare1 = GameManager.instance.activeSave.jumpscare1;
                SaveManager.instance.activeSave.jumpscare2 = GameManager.instance.activeSave.jumpscare2;
                SaveManager.instance.activeSave.jumpscare3 = GameManager.instance.activeSave.jumpscare3;
                SaveManager.instance.activeSave.jumpscare4 = GameManager.instance.activeSave.jumpscare4;
                SaveManager.instance.activeSave.jumpscare5 = GameManager.instance.activeSave.jumpscare5;
                //Flashbacks
                SaveManager.instance.activeSave.flashback1 = GameManager.instance.activeSave.flashback1;
                SaveManager.instance.activeSave.flashback2 = GameManager.instance.activeSave.flashback2;
                SaveManager.instance.activeSave.flashback3 = GameManager.instance.activeSave.flashback3;
                SaveManager.instance.activeSave.flashback4 = GameManager.instance.activeSave.flashback4;
                SaveManager.instance.activeSave.flashback5 = GameManager.instance.activeSave.flashback5;
                //Batteries
                SaveManager.instance.activeSave.battery1 = GameManager.instance.activeSave.battery1;
                SaveManager.instance.activeSave.battery2 = GameManager.instance.activeSave.battery2;
                SaveManager.instance.activeSave.battery3 = GameManager.instance.activeSave.battery3;
                SaveManager.instance.activeSave.battery4 = GameManager.instance.activeSave.battery4;
                SaveManager.instance.activeSave.battery5 = GameManager.instance.activeSave.battery5;
                //Dialogues
                SaveManager.instance.activeSave.monologue1 = GameManager.instance.activeSave.monologue1;
                SaveManager.instance.activeSave.monologue2 = GameManager.instance.activeSave.monologue2;
                SaveManager.instance.activeSave.monologue3 = GameManager.instance.activeSave.monologue3;
                SaveManager.instance.activeSave.monologue4 = GameManager.instance.activeSave.monologue4;
                SaveManager.instance.activeSave.monologue5 = GameManager.instance.activeSave.monologue5;
                SaveManager.instance.activeSave.monologue6 = GameManager.instance.activeSave.monologue6;
                SaveManager.instance.activeSave.monologue7 = GameManager.instance.activeSave.monologue7;
                SaveManager.instance.activeSave.monologue8 = GameManager.instance.activeSave.monologue8;
                SaveManager.instance.activeSave.monologue9 = GameManager.instance.activeSave.monologue9;
                SaveManager.instance.activeSave.monologue10 = GameManager.instance.activeSave.monologue10;
                SaveManager.instance.activeSave.monologue11 = GameManager.instance.activeSave.monologue11;
                SaveManager.instance.activeSave.monologue12 = GameManager.instance.activeSave.monologue12;
                SaveManager.instance.activeSave.monologue13 = GameManager.instance.activeSave.monologue13;
                SaveManager.instance.activeSave.monologue14 = GameManager.instance.activeSave.monologue14;
                SaveManager.instance.activeSave.monologue15 = GameManager.instance.activeSave.monologue15;
                //Objectives
                SaveManager.instance.activeSave.objective1 = GameManager.instance.activeSave.objective1;
                SaveManager.instance.activeSave.objective2 = GameManager.instance.activeSave.objective2;
                SaveManager.instance.activeSave.objective3 = GameManager.instance.activeSave.objective3;
                SaveManager.instance.activeSave.objective4 = GameManager.instance.activeSave.objective4;
                SaveManager.instance.activeSave.objective5 = GameManager.instance.activeSave.objective5;
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