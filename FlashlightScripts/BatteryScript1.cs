using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
[Serializable()]
public class BatteryScript1 : ScriptableObject
{
    public bool tookBattery;
    public bool TookBattery { get { return tookBattery; } }
    public bool MyProperty
    {
        get { return tookBattery; }
        set { tookBattery = value; }
    }
    public void PickupBattery()
    {
        Flashlight_PRO.instance.batteries += 1;
        SaveManager.instance.activeSave.battery1 = true;
        batteryCount.instance.UpdateBatteries();
        tookBattery = true;
    }
}
