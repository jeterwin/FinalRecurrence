using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class key : MonoBehaviour
{
    public bool Key;
    public UnityEvent Event;
    public void SetActiveKey()
    {
        Key = true;
    }
    public void CheckKey()
    {
        if (Key == true)
        {
            Event.Invoke();
        }
    }
}
