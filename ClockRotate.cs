using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotate : MonoBehaviour
{
    private bool coroutineAllowed;

    private int numberShown;

    private void Start()
    {
        coroutineAllowed = true;
    }
    public void Rotate1()
    {
        if (coroutineAllowed)
            StartCoroutine(RotateWheel());
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        transform.Rotate(0f, 0f, 30f);

        //yield return new WaitForSeconds(0.01f);

        coroutineAllowed = true;

        yield return null;
    }
}
