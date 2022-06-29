using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class Subtitles1 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
    private void Update()
    {
        //If the player stops the subtitles, we will de-activate the subtitle text that appears on-screen.
        if (SettingsMenu.instance.subDec == true)
            textBox.enabled = true;
        else
            textBox.enabled = false;
    }
    private void Start()
    {
        //If we already played this dialogue we will go ahead and de-activate it for resource purposes.
        if(SaveManager.instance.activeSave.monologue1 == true)
            this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //If we didn't play this dialogue already, we will play it and save that we did.
        if (GameManager.instance.activeSave.monologue1 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                playableDirector.Play();
                GameManager.instance.activeSave.monologue1 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        playableDirector.Play();
        yield return null;
    }
}
