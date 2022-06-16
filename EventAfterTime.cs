using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventAfterTime : MonoBehaviour
{
    public UnityEvent Event;
    public float time;

    public void InvEv()
    {
        Event.Invoke();
    }
    public void InvokeEvents()
    {
        Invoke("InvEv", time);
    }
}
