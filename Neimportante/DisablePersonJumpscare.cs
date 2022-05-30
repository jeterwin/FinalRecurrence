using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePersonJumpscare : MonoBehaviour
{
    public GameObject Person;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PersonJumpScare")
        {
            Person.SetActive(false);
        }
    }
}
