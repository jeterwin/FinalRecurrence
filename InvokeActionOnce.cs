using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InvokeActionOnce : MonoBehaviour
{
    public UnityEvent @event;
    bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hasPlayed == false)
        {
            @event.Invoke();
            hasPlayed = true;
        }
    }
    public void CallFunction()
    {
        @event.Invoke();
    }
}
