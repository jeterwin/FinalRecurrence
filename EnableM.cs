using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableM : MonoBehaviour
{
    public CharController_Motor Mouvment;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Mouvment.enabled = true;
        }
    }
}
