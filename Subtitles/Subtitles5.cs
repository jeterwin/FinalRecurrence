using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Subtitles5 : MonoBehaviour
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
        if (SaveManager.instance.activeSave.monologue5 == true)
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
        if (GameManager.instance.activeSave.monologue5 == false)
        {
            if (other.gameObject.tag == "Player" && GameManager.instance.activeSave.note2 == true)
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue5 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        audioSource.Play();
        textBox.text = "I wonder where could cabin 7 be...it shouldn't be far from here.";
        yield return new WaitForSeconds(6f);
        textBox.text = "";
        this.enabled = false;
    }
}
