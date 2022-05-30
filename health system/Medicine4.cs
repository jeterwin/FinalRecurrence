using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine4 : MonoBehaviour
{
    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.medicine4 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void PickupBattery()
    {
        HealthSystem.instance.DrugsAmount += 1;
        GameManager.instance.activeSave.medicine4 = true;
        Destroy(this.gameObject);
        //batteryCount.instance.UpdateBatteries();
    }
}
