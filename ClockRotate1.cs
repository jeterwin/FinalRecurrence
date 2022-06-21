using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotate1 : MonoBehaviour
{
    public static ClockRotate1 instance;

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
        if (coroutineAllowed)
            StartCoroutine(RotateWheel());
        if (i <= 10)
            time = i + 2;
        else
            time = i - 10;
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        transform.Rotate(0f, 0f, 30f);

        if (i >= 12)
        {
            i = 1;
        }
        else
            i++;
        //yield return new WaitForSeconds(0.01f);

        coroutineAllowed = true;

        yield return null;
    }
}
