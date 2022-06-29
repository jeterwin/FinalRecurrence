using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class batteryCount : MonoBehaviour
{
    public static batteryCount instance;
    public TextMeshProUGUI textBox;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateBatteries()
    {
        //Call this function to update the current batteries text
        textBox.text = Flashlight_PRO.instance.batteries + "x";
    }
}
