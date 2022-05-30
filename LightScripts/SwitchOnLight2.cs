using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnLight2 : MonoBehaviour
{
    public GameObject light2;
    public GameObject switch2;
    //default pos x = -65
    private bool on = false;
    public AudioSource onSound;
    public AudioSource offSound;
    public void SwitchLight()
    {
        if(on == false)
        {
            light2.SetActive(true);
            on = true;
            switch2.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
            onSound.Play();
        }
        else
        {
            light2.SetActive(false);
            on = false;
            switch2.transform.rotation = Quaternion.Euler(new Vector3(-65f, -90f, 0f));
            offSound.Play();
        }
    }
}
