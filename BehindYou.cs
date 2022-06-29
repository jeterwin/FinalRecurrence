using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindYou : MonoBehaviour
{
    public Transform other;
    //Object to be invisible when you turn
    public GameObject toBeVisible;
    //In case a script should get disabled after you turn
    public AheadOfYou ndScript;
    public bool isInvisible = false;

    void Update()
    {
        if (other)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = other.position - transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                if (isInvisible == true)
                {
                    other.gameObject.SetActive(false);
                    this.GetComponent<BehindYou>().enabled = false;
                    toBeVisible.gameObject.SetActive(true);
                    ndScript.enabled = true;
                }
            }
            else
            {
                if(isInvisible == false)
                {
                    isInvisible = true;
                }
            }
        }
    }
}
