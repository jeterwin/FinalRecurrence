using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //Variables on how much and how fast the door can be opened
    public float ySensitivity;
    public float frontOpenPosLimit;
    public float backOpenPosLimit;

    public GameObject frontDoorCollider;
    public GameObject backDoorCollider;
    public Camera mainCamera;
    float yRot = 0;
    float lastRot = 0;
    DoorCollision doorCollision = DoorCollision.NONE;
    public Image crosshair;
    bool moveDoor = false;
    [Space]
    [Header("Audio")]
    public AudioSource closeDoor;
    public AudioClip openDoorSFX;

    void Awake()
    {
        StartCoroutine(doorMover());
    }
    void Update()
    {
        //Set a ray starting from the camera's middle
        Ray ray =  mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
 
        if (Input.GetMouseButtonDown(0))
        {
            if(PauseMenu.IsGamePaused == true)
                return;
            RaycastHit hitInfo;
            //Draw a ray from the ray's starting point forwards with a specified distance
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 2.5f))
            {
                //If the object hit was either the front door collider or the back door collider play a quick door opening sound
                //along with specifying which collision was hit
                if (hitInfo.collider.gameObject == frontDoorCollider)
                {
                    moveDoor = true;
                    doorCollision = DoorCollision.FRONT;
                    Fps_Script.instance.canRotate = false;
                    Fps_Script.instance.walkingSpeed = 1f;
                    Fps_Script.instance.runningSpeed = 1.5f;
                    if(closeDoor != null)
                    closeDoor.PlayOneShot(openDoorSFX);
                }
                else if (hitInfo.collider.gameObject == backDoorCollider)
                {
                    moveDoor = true;
                    doorCollision = DoorCollision.BACK;
                    Fps_Script.instance.canRotate = false;
                    Fps_Script.instance.walkingSpeed = 1f;
                    Fps_Script.instance.runningSpeed = 1.5f;
                    if (closeDoor != null)
                        closeDoor.PlayOneShot(openDoorSFX);
                }
                else
                {
                    doorCollision = DoorCollision.NONE;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Move at the normal speed after releasing the click
            moveDoor = false;
            Fps_Script.instance.canRotate = true;
            Fps_Script.instance.walkingSpeed = 1.5f;
            Fps_Script.instance.runningSpeed = 2f;
        }
    }

    IEnumerator doorMover()
    {
        bool stoppedBefore = false;
        while (true)
        {
            if (moveDoor)
            {
                stoppedBefore = false;
                //Rotate the door's local angles depending on the location and the mouse's Y axis
                //And make sure the rotation doesn't go past the opening limit
                if (doorCollision == DoorCollision.FRONT)
                {
                    yRot += -Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
                    yRot = Mathf.Clamp(yRot, 0, backOpenPosLimit);
                    transform.localEulerAngles = new Vector3(0, yRot, 0);
                }
                else if (doorCollision == DoorCollision.BACK)
                {
                    yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
                    yRot = Mathf.Clamp(yRot, 0, backOpenPosLimit);
                    transform.localEulerAngles = new Vector3(0, yRot, 0);
                }
            }
            else
            {
                if (!stoppedBefore)
                {
                    lastRot = yRot;
                    if (yRot == 0 && lastRot != yRot)
                        if (closeDoor != null)
                            closeDoor.Play();
                    stoppedBefore = true;
                }
            }

            yield return null;
        }

    }

    enum DoorCollision
    {
        NONE, FRONT, BACK
    }
}
