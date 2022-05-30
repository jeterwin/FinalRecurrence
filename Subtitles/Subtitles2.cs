using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles2 : MonoBehaviour
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
    private void Update()
    {
        if (SettingsMenu.instance.subDec == true)
            textBox.enabled = true;
        else
            textBox.enabled = false;
    }
    private void Start()
    {
        if (SaveManager.instance.activeSave.monologue2 == true)
            this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.activeSave.monologue2 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue2 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        audioSource.Play();
        textBox.text = "The road is blocked..Looks like I can only go right.";
        yield return new WaitForSeconds(4f);
        textBox.text = "";
        this.enabled = false;
    }
}
