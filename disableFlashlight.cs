using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableFlashlight : MonoBehaviour
{
    public void disableFlashlightFunction()
    {
        //If the flashlight is on, turn it off and force the player to be unable to turn it on back again
        if(Flashlight_PRO.instance.is_enabled == true)
        {
            Flashlight_PRO.instance.StartCoroutine(Flashlight_PRO.instance.FlashlightOnOff());
        }
        SaveManager.instance.activeSave.hasFlashlight = false;
    }
    public void enableFlashlight()
    {
        SaveManager.instance.activeSave.hasFlashlight = true;
    }
}
