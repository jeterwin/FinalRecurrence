using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class IfPressE : MonoBehaviour
{   //Variables
    public UnityEvent @event;
    public UnityEvent ExitEvent;
    private bool InCollider;
    private bool Escape = false;
    //In this function we check if the player is in the right place and press the "E" or "Q" key
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
    //In this function we check if the player has reached the right place
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InCollider = true;
        }

    }
    //In this function we check if the player has left the perimeter where he can press the "E" or "Q" key.
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
