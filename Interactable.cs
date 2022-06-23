using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Cinemachine;
public class Interactable : MonoBehaviour
{
    public UnityEvent @event;
    public static Interactable instance;
    public UnityEvent pressE;
    public UnityEvent pressQ;
    public UnityEvent mouseClick;
    public UnityEvent Escape;
    public Sprite interactIcon;
    public PlayableDirector director;
    public CinemachineVirtualCamera vcam;
    public int ID;
    public bool canInteract = true;

    //End level interactable door
    //public AudioSource cantLeaveYet;
    [Header("Leaving Variables")]
    public GameObject cantLeaveYetCanvas;
    public AudioSource cantLeaveYetAudio;

    //Pickup flashlight
    public GameObject crosshair;
    public GameObject crosshair1;
    public GameObject flashlight;
    public Collider collision;
    public GameObject batteryCanvas;
    public AudioSource flashlightAudio;
    private GameObject player1;

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
        player1 = GameObject.Find("CMvcam2");
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
        director.Play();
        @event.Invoke();
        vcam.Priority = 12;
        Fps_Script.instance.enabled = false;
        player1.GetComponent<HeadBop>().enabled = false;
        yield return null;
    }
    IEnumerator animationLMFAO()
    {
        cantLeaveYetCanvas.SetActive(true);
        cantLeaveYetAudio.Play();
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
