using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotate : MonoBehaviour
{
    public static ClockRotate instance;

    private bool coroutineAllowed;

    private int numberShown;

    public int i = 1, time;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        coroutineAllowed = true;
    }
    public void Rotate1()
    {
        //Don't allow the player to spam rotations
        if (coroutineAllowed)
            StartCoroutine(RotateWheel());
        //Quick maths so we can figure the right time on the clock
        if (i <= 4)
            time = i + 8;
        else
            time = i - 4;
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;
        //Rotate the wheel 30 degrees
        transform.Rotate(0f, 0f, 30f);

        if (i >= 12)
        {
            i = 1;
        }
        else
        i++;

        coroutineAllowed = true;

        yield return null;
    }
}
