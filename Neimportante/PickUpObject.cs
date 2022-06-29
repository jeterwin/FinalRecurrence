using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PickUpObject : MonoBehaviour
{
    //My hands aka the point where the object will get to
    public GameObject myHands;
    //Pretty explanatory
    GameObject ObjectIwantToPickUp;
    Vector3 originalPos;
    Quaternion originalRotation;
    private MeshCollider objectPickedCollider;
    bool hasItem;

    //The player, the post profile and the depth of field that will be modified while the item is picked and rotated
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
        objectPickedCollider = GetComponent<MeshCollider>();
    }
 
    public void Pickup()
    {
        //If the player has no item in hand, make its collider a trigger so it doesn't accidentally push the player away,
        //activate it's depth of field settings, make the object go from its initial position to the position of your hands
        //over a period of time, make the player unable to move and play an animation which shows the player the rotation controls
        if(hasItem == false)
        {
            objectPickedCollider.isTrigger = true;
            DoF = profile.GetSetting<DepthOfField>();
            DoF.active = true;
            StartCoroutine(LerpPositionTo(myHands.transform.position, 0.7f));
            playerScript.canMove = false;
            itemAnimator.Play(FadeInAnimName, 0, 0.0f);
        }
    }
    public void DropItem()
    {
        //If the player has an item in their hands, make the collider normal again, de-activate depth of field, lerp the
        //object position back to its original position and change its rotation to its original one, let the player move again
        //and make the rotation controls fade off the screen smoothly
        if (hasItem == true)
        {
            objectPickedCollider.isTrigger = false;
            DoF = profile.GetSetting<DepthOfField>();
            DoF.active = false;
            StartCoroutine(LerpPositionBack(originalPos, 0.7f));
            ObjectIwantToPickUp.transform.rotation = originalRotation;
            hasItem = false;
            playerScript.canMove = true;
            itemAnimator.Play(FadeOutAnimName, 0, 0.0f);
        }
    }
    IEnumerator LerpPositionTo(Vector3 targetPosition, float duration)
    {
        //Simple lerp script
        float time = 0;
        Vector3 startPosition = ObjectIwantToPickUp.transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        hasItem = true;
        transform.position = targetPosition;
    }
    IEnumerator LerpPositionBack(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = ObjectIwantToPickUp.transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    private void Update()
    {
        if(hasItem == true)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                DropItem();
            }
            h = 1.0f * Input.GetAxis("Mouse X");
            v = 1.0f * Input.GetAxis("Mouse Y");
            transform.Rotate(0, h, v);
        }
    }
}