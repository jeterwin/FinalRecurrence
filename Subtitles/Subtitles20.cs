using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class Subtitles20 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
    private void Start()
    {
        if (SaveManager.instance.activeSave.monologue20 == true)
            this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (SettingsMenu.instance.subDec == true)
            textBox.enabled = true;
        else
            textBox.enabled = false;
    }
    public void playCutscene()
    {
        playableDirector.Play();
        GameManager.instance.activeSave.monologue20 = true;
    }

}
