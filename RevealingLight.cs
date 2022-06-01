using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealingLight : MonoBehaviour
{
    public Transform tfLight;
    // Start is called before the first frame update
    void Start()
    {
        var goLight = GameObject.Find("Spotlight1");
        if (goLight) tfLight = goLight.transform;
    }

    void Update()
    {
        if (tfLight && Flashlight_PRO.instance.is_enabled)
        {
            GetComponent<Renderer>().material.SetVector("_LightPos", tfLight.position);
            GetComponent<Renderer>().material.SetVector("_LightDir", tfLight.forward);
        }
        else
        {
            GetComponent<Renderer>().material.SetVector("_LightPos", Vector3.zero);
            GetComponent<Renderer>().material.SetVector("_LightDir", Vector3.zero);
        }
    }
}
