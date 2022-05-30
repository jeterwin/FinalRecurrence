using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMovment : MonoBehaviour
{
    public CharController_Motor Mouvment;
    public GameObject TextSpace;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Mouvment.enabled = true;
            TextSpace.SetActive(false);
        }
    }
}
