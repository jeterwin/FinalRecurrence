using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class Subtitles2 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector playableDirector;
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
                playableDirector.Play();
                GameManager.instance.activeSave.monologue2 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        playableDirector.Play();
        yield return null;
/*        audioSource.Play();
        textBox.text = "The road is blocked..Looks like I can only go right.";
        yield return new WaitForSeconds(4f);
        textBox.text = "";
        this.enabled = false;*/
    }
}
