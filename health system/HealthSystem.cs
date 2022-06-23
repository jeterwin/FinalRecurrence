using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class HealthSystem : MonoBehaviour
{
    #region
    public static HealthSystem instance;
    public float maxHealthValue = 100;
    public float sanityValue = 20;
    public float ValueAdd = 20;
    public int DrugsAmount;
    public Image IconImage;
    public UnityEvent IfValue0_Event;
    public UnityEvent IfValue1_Event;
    #endregion 
    public void Start()
    {
        IconImage.fillAmount = sanityValue;
    }
    private void Awake()
    {
        instance = this;
    }
    public void Jumpscare(float minusSanity)
    {
        sanityValue -= minusSanity;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.H) && DrugsAmount > 0)
        {
            sanityValue += ValueAdd;
            DrugsAmount -= 1;
        }
        IconImage.fillAmount = sanityValue / 100;
        if (sanityValue < 30)
        {
            IfValue0_Event.Invoke();
        }
        else
        {
            IfValue1_Event.Invoke();
        }
    }

    public void PickUpDrugs()
    {
        DrugsAmount += 1;
    }
}
