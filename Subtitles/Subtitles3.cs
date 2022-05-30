using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles3 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public AudioSource audioSource;
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
        if (SaveManager.instance.activeSave.monologue3 == true)
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
        if(GameManager.instance.activeSave.monologue3 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue3 = true;
            }
        }

    }

    IEnumerator Sequence1()
    {
        audioSource.Play();
        textBox.text = "A wood cabin..I can't believe someone would live in this forest.";
        yield return new WaitForSeconds(5f);
        textBox.text = "";
        this.enabled = false;
    }
}
