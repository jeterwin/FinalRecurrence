using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public static Interactable instance;
    public UnityEvent pressE;
    public UnityEvent pressQ;
    public UnityEvent mouseClick;
    public Sprite interactIcon;
    public int ID;
    public bool canInteract = true;

    //End level interactable door
    //public AudioSource cantLeaveYet;
    public GameObject cantLeaveYetCanvas;

    //Pickup flashlight
    public GameObject crosshair;
    public GameObject crosshair1;
    public GameObject flashlight;
    public Collider collision;
    public GameObject batteryCanvas;
    public AudioSource flashlightAudio;

    public Animator animator;
    public string animationName;
    // Start is called before the first frame update
    void Start()
    {
        ID = Random.Range(0, 999999);
    }

    private void Awake()
    {
        instance = this;
    }
   public void EndLevel1()
   {
        if(SaveManager.instance.activeSave.hasFlashlight == true && SaveManager.instance.activeSave.canOpenInventory == true)
        {
            StartCoroutine(leaveLevel());
            //SaveManager.instance.Save();
        }
        else
        {
            if(canInteract == true)
            {
            StartCoroutine(animationLMFAO());
            canInteract = false;
            }
        }
   }
    IEnumerator leaveLevel()
    {
        GameObject.Find("LeaveDoor").GetComponent<Interactable>().enabled = false;
        crosshair.SetActive(false);
        crosshair1.SetActive(false);
        Fps_Script.instance.canMove = false;
        Fps_Script.instance.canRotate = false;
        //animator.Play(animationName);
        //yield return new WaitForSeconds(2f);
        PauseMenu.instance.canPause = false;
        SaveManager.instance.activeSave.hasFlashlight = false;
        //SaveManager.instance.activeSave.level = 3;
        InGameLevelLoader.instance.LoadLevelWithoutAudio(3);
        yield return null;
    }
    IEnumerator animationLMFAO()
    {
        cantLeaveYetCanvas.SetActive(true);
        yield return new WaitForSeconds(7f);
        cantLeaveYetCanvas.SetActive(false);
        canInteract = true;
        yield return null;
    }
    public void PickupFlashlight()
    {
        SaveManager.instance.activeSave.hasFlashlight = true;
        flashlight.SetActive(true);
        flashlightAudio.Play();
        Destroy(collision.gameObject);
        batteryCanvas.gameObject.SetActive(true);
        batteryCount.instance.UpdateBatteries();
        //SaveManager.instance.Save(); //Pe cand o fi lansat jocu
    }  
}
