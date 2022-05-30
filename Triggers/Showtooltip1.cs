using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showtooltip1 : MonoBehaviour
{
    public GameObject pickupitem1;
    private bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasPlayed)
        {
            pickupitem1.SetActive(true);
            hasPlayed = true;
        }
    }
}
