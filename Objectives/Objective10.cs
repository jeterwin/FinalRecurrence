using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Objective10 : MonoBehaviour
{
    bool hasPlayed = false;
    public string text;
    public Text objectiveText;
    public Text currentObjectiveText;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject objectiveCanvas;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (SaveManager.instance.hasLoaded)
        {
            if (SaveManager.instance.activeSave.objective10 == false)
            {
                if (other.gameObject.tag == "Player" && hasPlayed == false)
                {
                    objectiveText.text = text;
                    currentObjectiveText.text = text;
                    StartCoroutine(disable());
                    GameManager.instance.activeSave.currentObjective = text;
                    GameManager.instance.activeSave.objective10 = true;
                    //SaveManager.instance.Save();
                }
            }
        }

    }
    IEnumerator disable()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        objectiveCanvas.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        objectiveCanvas.SetActive(false);
        Destroy(this.gameObject);
        yield return null;
    }
}
