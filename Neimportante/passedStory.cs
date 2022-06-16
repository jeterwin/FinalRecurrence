using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Footsteps;

public class passedStory : MonoBehaviour
{
    public PlayableDirector director;
    public Animator animator;
    public GameObject changeAmbientGroup;
    public string batteryCanvas;
    private void Start()
    {
        if(SaveManager.instance.activeSave.passedStory == false)
        {
            director.Play();
        }
        else
        {
            Fps_Script.instance.canMove = true;
            Fps_Script.instance.canRotate = true;
            changeAmbientGroup.SetActive(true);
            animator.Play(batteryCanvas);
            PauseMenu.instance.canPause = true;
            batteryCount.instance.UpdateBatteries();
            GetComponent<CharacterFootsteps>().enabled = true;
        }
    }
}

