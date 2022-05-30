using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioJumpScare : MonoBehaviour
{
    public AudioSource Audio;
    public AudioSource Audio1;
    public bool disable = false;
    public float TimeToDisable = 3;
    public float TimeToPlay;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("Enable", TimeToPlay);
            Invoke("Disable", TimeToDisable);
        }

    }
    public void PlayAudio()
    {
        Audio.Play();
        if (Audio1 != null)
            Audio1.Play();
        Invoke("Disable", TimeToDisable);
    }
    public void Enable()
    {
        Audio.Play();
        if (Audio1 != null)
            Audio1.Play();
    }    
    public void Disable()
    {
        if (disable == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
