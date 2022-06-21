using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PickUpObject : MonoBehaviour
{
    public GameObject myHands;
    GameObject ObjectIwantToPickUp;
    Vector3 originalPos;
    Quaternion originalRotation;
    private MeshCollider objectPickedCollider;
    bool hasItem;

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