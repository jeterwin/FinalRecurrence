using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.instance.canPause = true;
    }
    public void cantPause()
    {
        PauseMenu.instance.canPause = false;
    }
}