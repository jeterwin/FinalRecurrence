using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadeAudioSource : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float durationOut, durationIn, TargetVolumeOut, TargetVolumeIn;
    public bool fadeOut = false, playingAudio = false;
    public void AudioSourceFadeOut()
    {
        if(fadeOut == false)
        StartCoroutine(StartFadeOut(audioSource, durationOut, TargetVolumeOut));
        playingAudio = false;
    }
    public void AudioSourceFadeIn()
    {
        if(fadeOut == true)
        StartCoroutine(StartFadeIn(audioSource, durationIn, TargetVolumeIn));
        if (fadeOut == false && playingAudio == false)
        {
            audioSource.PlayOneShot(clip);
            StartCoroutine(PlayHeartBeat(clip.length));
            playingAudio = true;
        }
    }
    public IEnumerator StartFadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        fadeOut = true;
        yield break;
    }
    public IEnumerator StartFadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        fadeOut = false;
        yield break;
    }
    public IEnumerator PlayHeartBeat(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        playingAudio = false;
    }
    //StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
}