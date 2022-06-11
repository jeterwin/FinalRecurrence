using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadeAudioSource : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip[] differentWhispers;
    [Space]
    [Header("Parameters")]
    public float durationOut, durationIn, TargetVolumeOut, TargetVolumeIn, timeToRepeat;
    public bool fadedOut = false, playingAudio = false;
    [SerializeField] public bool randomSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && fadedOut == false)
        {
            AudioSourceFadeOut();
        }
    }
    private void Start()
    {
        if (randomSound == true)
            PlayRandomClip();
    }
    void toBeInvoked()
    {
        Invoke("PlayRandomClip", timeToRepeat);
    }    
    void PlayRandomClip()
    {
        audioSource.clip = differentWhispers[Random.Range(0, differentWhispers.Length)];
        audioSource.Play();
        toBeInvoked();
    }
    public void AudioSourceFadeOut()
    {
        if(fadedOut == false)
        StartCoroutine(StartFadeOut(audioSource, durationOut, TargetVolumeOut));
        playingAudio = false;
    }
    public void AudioSourceFadeIn()
    {
        if(fadedOut == true)
        StartCoroutine(StartFadeIn(audioSource, durationIn, TargetVolumeIn));
        if (fadedOut == false && playingAudio == false)
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
        fadedOut = true;
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
        fadedOut = false;
        yield break;
    }
    public IEnumerator PlayHeartBeat(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        playingAudio = false;
    }
    //StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
}