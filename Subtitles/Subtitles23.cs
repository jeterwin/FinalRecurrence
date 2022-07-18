using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class Subtitles23 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
    private void Start()
    {
        if (SaveManager.instance.activeSave.monologue23 == true)
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
        if (GameManager.instance.activeSave.monologue23 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                playableDirector.Play();
                GameManager.instance.activeSave.monologue23 = true;
            }
        }
    }
    public void playCutscene()
    {
        playableDirector.Play();
        GameManager.instance.activeSave.monologue23 = true;
    }
}
