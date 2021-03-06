using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventAfterTime : MonoBehaviour
{
    public UnityEvent Event;
    public UnityEvent Event1;
    public UnityEvent Event2;
    public float time;

    public void InvEv()
    {
        Event.Invoke();
    }
    public void InvokeEvents()
    {
        //Call the specified functions after a period of time
        Invoke("InvEv", time);
    }
    public void InvEv1()
    {
        Event1.Invoke();
    }
    public void InvEv2()
    {
        Event2.Invoke();
    }
}
