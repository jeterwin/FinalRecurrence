using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles1 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public AudioSource MCSource1;
    public AudioSource MCSource2;
    public AudioSource MCSource3;
    public AudioSource MCSource4;

    public AudioSource AlexSource1;
    public AudioSource AlexSource2;
    public AudioSource AlexSource3;
    public AudioSource AlexSource4;
    public AudioSource AlexSource5;
    public AudioSource phoneRing;
    public AudioSource stopCall;
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
        if(SaveManager.instance.activeSave.monologue1 == true)
            this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(SaveManager.instance.activeSave.monologue1 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.monologue1 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        phoneRing.Play();
        yield return new WaitForSeconds(2f);
        MCSource1.Play();
        textBox.text = "Hey, perfect timing";
        yield return new WaitForSeconds(2f);
        MCSource2.Play();
        textBox.text = "I just got into the forest and I wanna leave already.";
        yield return new WaitForSeconds(3f);
        textBox.color = new Color32(255, 204, 145, 255);
        AlexSource1.Play();
        textBox.text = "Alex: Why is that?";
        yield return new WaitForSeconds(2f);
        textBox.color = new Color32(255, 255, 255, 255);
        MCSource3.Play();
        textBox.text = "I don't know. Something just doesn't feel right.";
        yield return new WaitForSeconds(3f);
        textBox.color = new Color32(255, 204, 145, 255);
        AlexSource2.Play();
        textBox.text = "Alex: Relax, maybe you are just paranoid.";
        yield return new WaitForSeconds(3f);
        AlexSource3.Play();
        textBox.text = "Anyways, we've reached a camp site and will be going forward.";
        yield return new WaitForSeconds(3f);
        AlexSource4.Play();
        textBox.text = "We left you two batteries for you on the table, they might prove useful.";
        yield return new WaitForSeconds(3f);
        AlexSource5.Play();
        textBox.text = "I'll keep updating you if anything happens.";
        yield return new WaitForSeconds(3f);
        textBox.color = new Color32(255, 255, 255, 255);
        MCSource4.Play();
        textBox.text = "Sure, I'm just hoping the camp site isn't that far..";
        yield return new WaitForSeconds(3f);
        textBox.text = "";
        this.enabled = false;
    }
}
