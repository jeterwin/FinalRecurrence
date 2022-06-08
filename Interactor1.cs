using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor1 : MonoBehaviour
{
    public LayerMask interactableLayermask;
    public LayerMask interactableLayermask1;
    public Camera mainCamera;
    Interactable interactable;
    public Image crosshair;
    public Image crosshairSmall;
    public Sprite defaultIcon;
    public Sprite doorIcon;

    //public Material Curent_Selectable_Color_Default;
    // Start is called before the first frame update
    void Start()
    {
        if(!crosshair) return;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 10f, interactableLayermask))
        {
            if(hit.collider.GetComponent<Interactable>() != false)
            {
                if(interactable == null)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                }
                if(interactable.interactIcon != null)
                {
                    crosshair.enabled = true;
                    crosshairSmall.enabled = false;
                    crosshair.color = new Color32(255,255,255,255);
                    crosshair.sprite = interactable.interactIcon;
                }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.pressE.Invoke();
                }
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    interactable.pressQ.Invoke();
                }
                if(Input.GetMouseButtonDown(0))
                {
                    interactable.mouseClick.Invoke();
                }
            }
        }
        else
        {
            Ray ray =  mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
            if(Physics.Raycast(ray.origin, ray.direction, out hit, 10f))
            {
                if(hit.collider.gameObject.name == "Back" || hit.collider.gameObject.name == "Front")
                {
                crosshair.enabled = true;
                crosshairSmall.enabled = false;
                crosshair.color = new Color32(255,255,255,255);
                crosshair.sprite = doorIcon;
                }
                else
                {
                    crosshair.enabled = false;
                    crosshairSmall.enabled = true;
                    crosshair.color = new Color32(0,0,0,0);
                    interactable = null;
                }

            }
           else
            {
                crosshair.enabled = false;
                crosshairSmall.enabled = true;
                crosshair.color = new Color32(0,0,0,0);
                interactable = null;
            }
        }
    }
}
