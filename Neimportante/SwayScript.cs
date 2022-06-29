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
        //Get the local position of the flashlight
        vector3 = flashLight.localPosition;
    }
    private void Update()
    {
        //Only sway the flashlight if it is active (makes sense)
        if(SaveManager.instance.activeSave.hasFlashlight)
        {
            float fx  = -Input.GetAxis("Mouse X") * xSwayAmount;
            float fy  = -Input.GetAxis("Mouse Y") * ySwayAmount;
     
            //Make sure the sway amount variables don't go over the max sway amount you want
            //if they do it would probably not be the best :)
            if(fx > maxXAmount)
            fx = maxXAmount;
     
            if(fx < -maxXAmount)
            fx = maxXAmount;
     
            if(fy > maxYAmount)
            fy = maxYAmount;
     
            if(fy < -maxYAmount)
            fy = -maxYAmount;
     
            //Make a Vector3 which gets the flashlight's x, y and z position values and adds the sway amount to it
            //After that go from the flashlight's local position to the new Vector3 over time
            Vector3 detection = new Vector3(vector3.x + fx, vector3.y + fy, vector3.z);
            flashLight.localPosition = Vector3.Lerp(flashLight.localPosition, detection, Time.deltaTime * smooth);
        }
    }
}
