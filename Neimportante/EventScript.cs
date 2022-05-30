using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventScript : MonoBehaviour
{
    public UnityEvent Event;
    public UnityEvent Event2;
    public void Events()
    {
        Event.Invoke();
    }
    public void Events2()
    {
        Event2.Invoke();
    }

}
