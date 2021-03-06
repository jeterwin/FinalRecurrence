using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySourceAfterTime : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Play", 2f, audioSource.clip.length * Random.Range(10, 20));
    }
    public void Play()
    {
        audioSource.Play();
    }
}
