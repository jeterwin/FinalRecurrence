using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showtooltip3 : MonoBehaviour
{
    public GameObject pickupitem3;
    private bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasPlayed)
        {
            pickupitem3.SetActive(true);
            hasPlayed = true;
        }
    }
}
