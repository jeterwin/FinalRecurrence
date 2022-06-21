using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwapReturn : MonoBehaviour
{
    public AudioSource newTrack;
    public float volume1, volume2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.SwapTrack(newTrack, volume1, volume2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.ReturnToDefault();
        }
    }
}
