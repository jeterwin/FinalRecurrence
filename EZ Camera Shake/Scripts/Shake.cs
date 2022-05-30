using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class Shake : MonoBehaviour
{
    public CameraShaker camera;
    public void ShakeC()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        Invoke("DisableAf", 1.5f);
    }
    public void DisableAf()
    {
        camera.enabled = false;
    }
}
