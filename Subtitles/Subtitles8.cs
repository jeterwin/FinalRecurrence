using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles8 : MonoBehaviour
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
        if (SaveManager.instance.activeSave.monologue8 == true)
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
        if (GameManager.instance.activeSave.monologue8 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue8 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        audioSource.Play();
        textBox.text = "What is that man doing in the forest at this time?";
        yield return new WaitForSeconds(4f);
        audioSource1.Play();
        textBox.text = "I better follow him in that cabin.";
        yield return new WaitForSeconds(4f);
        textBox.text = "";
        this.enabled = false;
    }
}
