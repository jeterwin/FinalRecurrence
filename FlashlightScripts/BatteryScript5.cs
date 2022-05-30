using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript5 : MonoBehaviour
{
    private void Start()
    {
        if(SaveManager.instance.hasLoaded)
        {
            if(SaveManager.instance.activeSave.battery5 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void PickupBattery()
    {
        Flashlight_PRO.instance.batteries += 1;
        GameManager.instance.activeSave.battery5 = true;
        Destroy(this.gameObject);
        batteryCount.instance.UpdateBatteries();
    }
}
