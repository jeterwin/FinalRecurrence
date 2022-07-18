using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BatteryScript : MonoBehaviour
{
    public bool tookBattery;
    public BatteryScript1 batteryValue;
    public void PickupBattery()
    {
        Flashlight_PRO.instance.batteries += 1;
        SaveManager.instance.activeSave.battery1 = true;
        batteryCount.instance.UpdateBatteries();
        tookBattery = true;
        batteryValue.tookBattery = true;
    }
}
