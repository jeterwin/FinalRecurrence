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
    }
}
