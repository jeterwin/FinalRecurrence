using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Footsteps;

public class canMove : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject g;
    private CharacterFootsteps characterFootsteps;
    private void Awake()
    {
        //Find the character footsteps script on the player and enable it when the gameobject this script is attached
        //to gets activated
        g = GameObject.Find("Player");
        characterFootsteps = g.GetComponent<CharacterFootsteps>();
    }
    void Start()
    {
        Fps_Script.instance.canMove = true;
        Fps_Script.instance.canRotate = true;
        characterFootsteps.enabled = true;
    }
}
