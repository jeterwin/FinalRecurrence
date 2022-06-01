using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Piano : MonoBehaviour
{
    public int PianoK = 0;
    public UnityEvent @event;
    public void PianoKUp()
    {
        PianoK += 1;
    }
    public void CheckPiano()
    {
        if (PianoK == 5)
        {
            @event.Invoke();
        }
    }
}
