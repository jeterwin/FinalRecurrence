using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective3 : MonoBehaviour
{
    public string On;
    public string text;
    public Animator animator;
    public Text objectiveText;
    public Text currentObjectiveText;
    private void OnTriggerEnter(Collider other)
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.objective3 == false)
            {
                if (other.gameObject.tag == "Player")
                {
                    objectiveText.text = text;
                    currentObjectiveText.text = text;
                    animator.Play(On);
                    GameManager.instance.activeSave.currentObjective = text;
                    GameManager.instance.activeSave.objective3 = true;
                    //SaveManager.instance.Save();
                }
            }
        }

    }
}
