using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AheadOfYou : MonoBehaviour
{
    public Transform other;
    public GameObject toBeInvisible;
    bool isInvisible = false;

    void Update()
    {
        if (other)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = other.position - transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                if (isInvisible == false)
                {
                    isInvisible = true;
                }
            }
            else
            {
                if (isInvisible == true)
                {
                    other.gameObject.SetActive(true);
                    this.GetComponent<AheadOfYou>().enabled = false;
                    if(toBeInvisible != null)
                    toBeInvisible.SetActive(false);
                }
            }
        }
    }
}
