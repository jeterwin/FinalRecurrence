using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective2 : MonoBehaviour
{
    bool hasPlayed = false;
    public string On;
    public string text;
    public Animator animator;
    public Text objectiveText;
    public Text currentObjectiveText;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.objective2 == false)
            {
                if (other.gameObject.tag == "Player" && hasPlayed == false)
                {
                    objectiveText.text = text;
                    currentObjectiveText.text = text;
                    StartCoroutine(disable());
                    animator.Play(On);
                    GameManager.instance.activeSave.currentObjective = text;
                    GameManager.instance.activeSave.objective2 = true;
                    //SaveManager.instance.Save();
                }
            }
        }

    }
    IEnumerator disable()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        yield return null;
    }
}
