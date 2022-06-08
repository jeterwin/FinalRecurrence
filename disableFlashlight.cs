using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableFlashlight : MonoBehaviour
{
    public void disableFlashlightFunction()
    {
        if(Flashlight_PRO.instance.is_enabled == true)
        {
            Flashlight_PRO.instance.StartCoroutine(Flashlight_PRO.instance.FlashlightOnOff());
            SaveManager.instance.activeSave.hasFlashlight = false;
        }
    }
}
