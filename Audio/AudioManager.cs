using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public AudioClip defaultAmbience;
    public AudioSource defaultAmbience;
    public AudioSource defaultAmbience1;

    //private AudioSource track01, track02;
    private bool isPlayingTrack01;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        //track01 = gameObject.AddComponent<AudioSource>();
        //track02 = gameObject.AddComponent<AudioSource>();
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
        SwapTrack(defaultAmbience, 0.3f, 0);
    }

    private IEnumerator FadeTrack(AudioSource newClip, float volume1, float volume2)
    {
        float timeToFade = 3f;
        float timeElapsed = 0;
        if(isPlayingTrack01)
        {
            //track02.clip = newClip;
            //track02.Play();
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
