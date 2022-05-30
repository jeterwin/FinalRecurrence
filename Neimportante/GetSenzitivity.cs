using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSenzitivity : MonoBehaviour
{
    public Slider SensitivitySld;
    public CharController_Motor fpsController;

    private void Update()
    {
        fpsController.sensitivity = PlayerPrefs.GetFloat("sens", 15f);
        SensitivitySld.value = PlayerPrefs.GetFloat("sens", 15f);
    }
}
