using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public GameObject @object;
    public GameObject First_object;
    public AudioSource turnAround;
    private void Update()
    {

        if (First_object.active == false)
        {
            Invoke("Disable", 2);
            Debug.Log("ex");
        }
    }
    public void Disable()
    {
        @object.SetActive(false);
    }
    public void OnBecameVisible()
    {
        Invoke("Disable", 1);
    }
    public void Diable2()
    {
        Invoke("Disable", 3);
        turnAround.Play();
    }
}
