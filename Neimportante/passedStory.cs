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
            //If the player didn't already see the story, play the story animation.
            director.Play();
        }
        else
        {
            //If they did, let them move and activate the rest that needs to be on screen.
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

