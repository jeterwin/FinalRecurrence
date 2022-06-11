using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrR : MonoBehaviour
{
    public Transform game;
    public float z;


    void Update()
    {
        game.transform.Rotate(new Vector3(-z, 0, 0)); //applying rotation
    }
}
