using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class ChangePostProcessVolume : MonoBehaviour
{
    public PostProcessVolume newVolume, oldVolume;
    public float duration, newVal;
    private ChromaticAberration chromaticAberration1;
    private void Awake()
    {
        chromaticAberration1 = newVolume.profile.GetSetting<ChromaticAberration>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(goTo1());
    }
    private void Update()
    {
        newVal = Random.Range(0.1f, 1);
        chromaticAberration1.intensity.value = newVal;
        // Calculate new smoothed average
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(comeTo1());
    }
    public IEnumerator goTo1()
    {
        float time = 0;
        while(time <= duration)
        {
            newVolume.weight = Mathf.Lerp(0, 1, time / 2);
            time += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator comeTo1()
    {
        float time = duration;
        while (time >= 0)
        {
            newVolume.weight = Mathf.Lerp(1, 0, time / 2);
            time -= Time.deltaTime;
            yield return null;
        }
    }
}
