using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    Vector3 originalPos;
    Quaternion originalRotation;
    bool hasItem; // a bool to see if you have an item in your hand

    public Fps_Script playerScript;
    public PostProcessProfile profile;
    private DepthOfField DoF;

    public Animator itemAnimator;
    public string FadeInAnimName;
    public string FadeOutAnimName;
   
    public float RotationSpeed = 5f;
    public float speed = 1f;
    private float v,h;

    void Start()
    {
        hasItem = false;
        ObjectIwantToPickUp = this.gameObject;
        originalPos = ObjectIwantToPickUp.transform.position;
        originalRotation = ObjectIwantToPickUp.transform.rotation;
    }
 
    public void Pickup()
    {
        if(hasItem == false)
        {
        hasItem = true;
            DoF = profile.GetSetting<DepthOfField>();
            DoF.active = true;
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
        ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
        ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
        playerScript.canMove = false;
        itemAnimator.Play(FadeInAnimName, 0, 0.0f);
        }
    }
    public void DropItem()
    {
        if(hasItem == true)
        {
            DoF = profile.GetSetting<DepthOfField>();
            DoF.active = false;
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            ObjectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands
            ObjectIwantToPickUp.transform.rotation = originalRotation;
            ObjectIwantToPickUp.transform.position = originalPos;
            hasItem = false;
            playerScript.canMove = true;
            itemAnimator.Play(FadeOutAnimName, 0, 0.0f);
        }
    }
    private void Update()
    {
        if(hasItem == true)
        {
            h = 2.0f * Input.GetAxis("Mouse X");
            v = 2.0f * Input.GetAxis("Mouse Y");
            transform.Rotate(v, h, 0);
        }
    }
}