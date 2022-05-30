using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayScript : MonoBehaviour
{
    public Transform flashLight;
    //public Transform mesh;
    public float xSwayAmount = 0.1f;
    public float ySwayAmount = 0.05f;
    public float maxXAmount = 0.35f;
    public float maxYAmount = 0.2f;
    private Vector3 vector3;
    public float smooth = 3.0f;

    private void Start()
    {
        vector3 = flashLight.localPosition;
    }
    private void Update()
    {
        if(SaveManager.instance.activeSave.hasFlashlight)
        {
        float fx  = -Input.GetAxis("Mouse X") * xSwayAmount;
        float fy  = -Input.GetAxis("Mouse Y") * ySwayAmount;
     
        if(fx > maxXAmount)
        fx = maxXAmount;
     
        if(fx< -maxXAmount)
        fx = maxXAmount;
     
        if(fy > maxYAmount)
        fy = maxYAmount;
     
        if(fy < -maxYAmount)
        fy = -maxYAmount;
     
        Vector3 detection = new Vector3(vector3.x + fx, vector3.y + fy, vector3.z);
        flashLight.localPosition = Vector3.Lerp(flashLight.localPosition, detection, Time.deltaTime * smooth);
        //mesh.localPosition = Vector3.Lerp(mesh.localPosition, detection, Time.deltaTime * smooth);
        }
    }
}
