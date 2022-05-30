using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmAnimPerson : MonoBehaviour
{
    public GameObject Person;
    public Animator Anim;
    public bool InTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Person.SetActive(true);
        }
    }
}
