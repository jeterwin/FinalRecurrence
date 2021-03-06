using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class Flashback3 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public PlayableDirector director;
    private void Start()
    {
        if (SaveManager.instance.activeSave.flashback3 == true)
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
        if (GameManager.instance.activeSave.flashback3 == false)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Sequence1());
                GameManager.instance.activeSave.flashback3 = true;
            }
        }
    }

    IEnumerator Sequence1()
    {
        director.Play();
        yield return new WaitForSeconds(10f);
        this.enabled = false;
    }
}
