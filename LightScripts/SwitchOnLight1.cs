using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnLight1 : MonoBehaviour
{
    public GameObject light1;
    public GameObject switch1;
    //default pos x = -65
    private bool on = false;
    public AudioSource onSound;
    public AudioSource offSound;
    public void SwitchLight()
    {
        if(on == false)
        {
            light1.SetActive(true);
            on = true;
            //switch1.transform.rotation = Quaternion.Euler(new Vector3(-65, 90f,0f));
            switch1.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0f));
            onSound.Play();
        }
        else
        {
            light1.SetActive(false);
            on = false;
            switch1.transform.rotation = Quaternion.Euler(new Vector3(-65, 90f, 0f));
            //switch1.transform.rotation = Quaternion.Euler(new Vector3(0,90f,0f));
            offSound.Play();
        }
    }
}
