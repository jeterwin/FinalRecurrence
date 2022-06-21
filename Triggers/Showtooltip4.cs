using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showtooltip4 : MonoBehaviour
{
    public GameObject pickupitem4;
    private bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasPlayed && SaveManager.instance.activeSave.hasFlashlight == true)
        {
            pickupitem4.SetActive(true);
            hasPlayed = true;
        }
    }
}
