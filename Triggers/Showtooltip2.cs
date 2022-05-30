using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showtooltip2 : MonoBehaviour
{
    public GameObject pickupitem2;
    private bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasPlayed)
        {
            pickupitem2.SetActive(true);
            hasPlayed = true;
        }
    }
}
