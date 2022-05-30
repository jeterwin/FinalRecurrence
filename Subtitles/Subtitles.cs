using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class Subtitles : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
/*    public AudioSource line1MC;
    public AudioSource line1Alex;
    public AudioSource line2MC;
    public AudioSource line2Alex;
    public AudioSource line3MC;
    public AudioSource line3Alex;
    public AudioSource line4MC;
    public AudioSource line5MC;*/

        private void Update()
    {
        if(SettingsMenu.instance.subDec == true)
        {
            textBox.enabled = true;
            return;
        }
        if(SettingsMenu.instance.subDec == false)
        {
            textBox.enabled = false;
            return;
        }
    }

    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Sequence1());
        if (SettingsMenu.instance.subDec == true)
            textBox.enabled = true;
        else
            textBox.enabled = false;
    }
    IEnumerator Sequence1()
    {
        playableDirector.Play();
        yield return null;
        /*line1MC.Play();
        textBox.text = "Ugh..";
        yield return new WaitForSeconds(1.5f);
        line2MC.Play();
        textBox.text  = "Who's calling me at this time?";
        yield return new WaitForSeconds(2.5f);
        line1Alex.Play();
        textBox.color = new Color32(255, 204, 145, 255);
        textBox.text  = "???: Morning sleepyhead.";
        yield return new WaitForSeconds(2.5f);
        textBox.color = new Color32(255, 255, 255, 255);
        line3MC.Play();
        textBox.text  = "Oh, it's you, Alex. What is so important that you had to wake me up at 4 AM?";
        yield return new WaitForSeconds(5.5f);
        line2Alex.Play();
        textBox.color = new Color32(255, 204, 145, 255);
        textBox.text = "Alex: Actually, I've got really important news. I called to let you know that me and Gabriel actually got a clue and might find a lead on the killer.";
        yield return new WaitForSeconds(8f);
        textBox.color = new Color32(255, 255, 255, 255);
        line4MC.Play();
        textBox.text = "Gonna meet at the usual place?";
        yield return new WaitForSeconds(3f);
        line3Alex.Play();
        textBox.color = new Color32(255, 204, 145, 255);
        textBox.text = "Alex: We are already here, hurry up.";
        yield return new WaitForSeconds(3f);
        textBox.text = "";
        yield return new WaitForSeconds(2f);
        textBox.color = new Color32(255, 255, 255, 255);
        line5MC.Play();
        textBox.text = "They are still searching up clues even after 2 years have passed, I can't be grateful enough to them.";
        yield return new WaitForSeconds(7f);
        textBox.text = "";*/
    }
}
