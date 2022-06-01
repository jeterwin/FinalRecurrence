using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkFlashlight : MonoBehaviour
{
    public AudioSource hasFlashlightNote;
    bool hasntPlayed = false;
    public void check()
    {
        if (SaveManager.instance.activeSave.hasFlashlight && SaveManager.instance.activeSave.note1 && hasntPlayed == false)
        {
            hasFlashlightNote.Play();
            hasntPlayed = true;
        }
    }
}
