using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwap : MonoBehaviour
{
    public AudioSource newTrack;
    public float volume1, volume2;
    bool hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && hasEntered == false)
        {
            AudioManager.instance.SwapTrack(newTrack, volume1, volume2);
            hasEntered = true;
        }
    }
}
