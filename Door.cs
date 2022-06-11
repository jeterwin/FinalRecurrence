using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public float ySensitivity = 300f;
    public float frontOpenPosLimit = 45;
    public float backOpenPosLimit = 45;

    public GameObject frontDoorCollider;
    public GameObject backDoorCollider;
    public Camera mainCamera;
    float yRot = 0;
    float lastRot = 0;
    DoorCollision doorCollision = DoorCollision.NONE;
    public Image crosshair;
    //Vector3 pos = new Vector3(-100, -100, 0);
    [Space]
    [Header("Audio")]
    bool moveDoor = false;
    public AudioSource closeDoor;
    public AudioClip openDoorSFX;
    bool isPlaying = false;


    // Use this for initialization
    void Awake()
    {
        StartCoroutine(doorMover());
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray =  mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
 
        if (Input.GetMouseButtonDown(0))
        {
            if(PauseMenu.IsGamePaused == true)
                return;
            //Debug.Log("Mouse down");
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 2.5f))
            {
                if (hitInfo.collider.gameObject == frontDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Front door hit");
                    doorCollision = DoorCollision.FRONT;
                    Fps_Script.instance.canRotate = false;
                    Fps_Script.instance.walkingSpeed = 1f;
                    Fps_Script.instance.runningSpeed = 1.5f;
                    closeDoor.PlayOneShot(openDoorSFX);
                }
                else if (hitInfo.collider.gameObject == backDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Back door hit");
                    doorCollision = DoorCollision.BACK;
                    Fps_Script.instance.canRotate = false;
                    Fps_Script.instance.walkingSpeed = 1f;
                    Fps_Script.instance.runningSpeed = 1.5f;
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
            moveDoor = false;
            //Debug.Log("Mouse up");
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
                //Debug.Log("Moving Door");
                //yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
                //Check if this is front door or back
                if (doorCollision == DoorCollision.FRONT)
                {
                    //Debug.Log("Pull Down(PULL TOWARDS)");
                    yRot += -Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
                    yRot = Mathf.Clamp(yRot, 0, backOpenPosLimit);
                    //yRot = Mathf.Clamp(yRot, -frontOpenPosLimit, 0);
                    //Debug.Log(yRot);
                    transform.localEulerAngles = new Vector3(0, yRot, 0);
                    //transform.localEulerAngles = new Vector3(0, -yRot, 0);
                }
                else if (doorCollision == DoorCollision.BACK)
                {
                    //Debug.Log("Pull Up(PUSH AWAY)");
                    //yRot = Mathf.Clamp(yRot, 0, backOpenPosLimit);
                    //Debug.Log(yRot);
                    //transform.localEulerAngles = new Vector3(0, yRot, 0);
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
                        closeDoor.Play();
                    stoppedBefore = true;
                    //Debug.Log("Stopped Moving Door");
                }
                if (lastRot != yRot && isPlaying == false)
                    StartCoroutine(PlayAudio());
            }

            yield return null;
        }

    }
    IEnumerator PlayAudio()
    {
        float i = Mathf.InverseLerp(0, frontOpenPosLimit, yRot);
        closeDoor.Play();
        closeDoor.time = i;
        isPlaying = true;
        yield return null;
    }

    enum DoorCollision
    {
        NONE, FRONT, BACK
    }
}
