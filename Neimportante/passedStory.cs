using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Footsteps;

public class passedStory : MonoBehaviour
{
    public UnityEvent @event;
    public PlayableDirector director;
    public Animator animator;
    public GameObject changeAmbientGroup;
    public string batteryCanvas;
    public BatteryScript1 battery1;
    private void Start()
    {
        if(SaveManager.instance.activeSave.passedStory == false)
        {
            //If the player didn't already see the story, play the story animation.
            director.Play();
            if (battery1.tookBattery == true)
                Flashlight_PRO.instance.batteries += 1;
        }
        else
        {
            //If they did, let them move and activate the rest that needs to be on screen.
            @event.Invoke();
            changeAmbientGroup.SetActive(true);
            animator.Play(batteryCanvas);
            PauseMenu.instance.canPause = true;
            batteryCount.instance.UpdateBatteries();
            GetComponent<CharacterFootsteps>().enabled = true;
        }
    }
}

