using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class GamePlayTriger : MonoBehaviour
{
    public UnityEvent Event;
    public UnityEvent ExitEvent;
    public TextMeshProUGUI text;
    [SerializeField]
    private bool InTrigger = false;
    public bool InGamePlay = false;
    public PauseMenu Menu;
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && InTrigger == true)
        {

            Event.Invoke();
            Menu.enabled = false;
        }
        if (InGamePlay)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.T))
        {
            InGamePlay = false;
        }
        if (InGamePlay == true && Input.GetKey(KeyCode.Escape))
        {
            ExitGamePlay();
            ExitEvent.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InTrigger = true;
            text.enabled = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        InTrigger = false;
        text.enabled = false;
    }
    public void InGamePlay_SetActive(bool In_Game)
    {
        InGamePlay = In_Game;
    }
    public void ExitGamePlay()
    {


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InGamePlay = false;
        Menu.enabled = true;

    }

}
