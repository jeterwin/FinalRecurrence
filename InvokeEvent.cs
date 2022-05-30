using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InvokeEvent : MonoBehaviour
{
    public UnityEvent @event;
    public UnityEvent @event2;
    public float InvokeAfterTime;
    public void InvokeAfterTimeF()
    {
        Invoke("InvokeEventF", InvokeAfterTime);
    }
    public void InvokeEventF()
    {
        @event.Invoke();
    }
    public void InvokeEventF1()
    {
        @event2.Invoke();
    }
    public void InvokeAfterTimeF1()
    {
        Invoke("InvokeEventF1", InvokeAfterTime);
    }
}
