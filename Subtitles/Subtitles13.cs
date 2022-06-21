using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
public class Subtitles13 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
    private void Start()
    {
        if (SaveManager.instance.activeSave.monologue13 == true)
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
        if (GameManager.instance.activeSave.monologue13 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                playableDirector.Play();
                GameManager.instance.activeSave.monologue13 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        playableDirector.Play();
        yield return null;
    }
}
