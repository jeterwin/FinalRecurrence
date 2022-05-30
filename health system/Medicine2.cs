using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine2 : MonoBehaviour
{
    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.medicine2 == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void PickupBattery()
    {
        HealthSystem.instance.DrugsAmount += 1;
        GameManager.instance.activeSave.medicine2 = true;
        Destroy(this.gameObject);
        //batteryCount.instance.UpdateBatteries();
    }
}
