using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class HealthSystem : MonoBehaviour
{
    #region
    public float maxHealthValue = 100;
    public float minHealthValue = 1;
    public float myValue = 20;
    public float ValueAdd = 20;
    public static HealthSystem instance;
    float changePerSecond;
    public float timeToChange = 15;
    public int DrugsAmount;
    public Image IconImage;
    public UnityEvent IfValue0_Event;
    public UnityEvent IfValue1_Event;
    #endregion 
    public void Start()
    {
        changePerSecond = (minHealthValue - maxHealthValue) / timeToChange;
        IconImage.fillAmount = maxHealthValue;
    }
    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        //myValue = Mathf.Clamp(myValue + changePerSecond * Time.deltaTime, minHealthValue, maxHealthValue);
        if (Input.GetKey(KeyCode.H) && DrugsAmount > 0)
        {
            myValue += ValueAdd;
            DrugsAmount -= 1;
        }
        IconImage.fillAmount = myValue / 100;
        if (myValue < 30)
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
