using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Footsteps;

public class canMove1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fps_Script.instance.canMove = true;
        Fps_Script.instance.canRotate = true;
    }
}
