using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class IfPressE : MonoBehaviour
{
    public UnityEvent @event;
    public UnityEvent ExitEvent;
    private bool InCollider;
    private bool Escape = false;

    private void Update()
    {
        if (InCollider && Input.GetKey(KeyCode.E))
        {
            @event.Invoke();
            Escape = true;
        }
        if (InCollider && Input.GetKey(KeyCode.Q))
        {
            ExitEvent.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InCollider = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Escape = false;
        InCollider = false;
    }
    public void BoolFalse()
    {
        InCollider = false;
    }
    public void BoolTrue()
    {
        InCollider = true;
    }

}
