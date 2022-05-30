using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles6 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    /*    private void Awake()
        {
            if (PlayerPrefs.GetInt("subtitles") == 1)
            {
                textBox.enabled = true;
                return;
            }

            else
            {
                textBox.enabled = false;
                return;
            }
        }*/
    private void Start()
    {
        if (SaveManager.instance.activeSave.monologue6 == true)
            this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (SettingsMenu.instance.subDec == true)
            textBox.enabled = true;
        else
            textBox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.activeSave.monologue6 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue6 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        audioSource.Play();
        textBox.text = "Not only does this place give me the creeps";
        yield return new WaitForSeconds(4f);
        audioSource1.Play();
        textBox.text = "But now I have to go through some forest trenches, great.";
        yield return new WaitForSeconds(4f);
        textBox.text = "";
        this.enabled = false;
    }
}
