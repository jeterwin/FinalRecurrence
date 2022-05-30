using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript1 : MonoBehaviour
{
    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.battery1 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void PickupBattery()
    {
        Flashlight_PRO.instance.batteries += 1;
        SaveManager.instance.activeSave.battery1 = true;
        Destroy(this.gameObject);
        batteryCount.instance.UpdateBatteries();
    }
}
