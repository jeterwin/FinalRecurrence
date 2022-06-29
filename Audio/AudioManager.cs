using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //The default ambience and the new one
    public AudioSource defaultAmbience;
    public AudioSource defaultAmbience1;

    private bool isPlayingTrack01;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        isPlayingTrack01 = true;

        SwapTrack(defaultAmbience, 0.3f, 0);
    }

    public void SwapTrack(AudioSource newClip, float volume1, float volume2)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip, volume1, volume2));
        isPlayingTrack01 = !isPlayingTrack01;
    }

    public void ReturnToDefault()
    {
        //Stop playing the current ambient sound and fade over the new default one
        SwapTrack(defaultAmbience, 0.3f, 0);
    }

    private IEnumerator FadeTrack(AudioSource newClip, float volume1, float volume2)
    {
        float timeToFade = 3f;
        float timeElapsed = 0;
        //Depending on the playing ambient, we will fade one or the other in a specified amount of time towards a specified volume
        if(isPlayingTrack01)
        {
            defaultAmbience = newClip;
            defaultAmbience.Play();

            while(timeElapsed < timeToFade)
            {
                defaultAmbience.volume = Mathf.Lerp(0, volume1, timeElapsed / timeToFade);
                defaultAmbience1.volume = Mathf.Lerp(volume2, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            defaultAmbience1.Stop();
        }
        else
        {
            defaultAmbience1 = newClip;
            defaultAmbience1.Play();

            while(timeElapsed < timeToFade)
            {
                defaultAmbience1.volume = Mathf.Lerp(0, volume1, timeElapsed / timeToFade);
                defaultAmbience.volume = Mathf.Lerp(volume2, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            defaultAmbience.Stop();
        }
    }
}
