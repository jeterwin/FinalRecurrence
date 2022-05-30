using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject playerObject;
    public bool canClimb = false;
    public float speed = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            canClimb = true;
        }
    }

        private void OnTriggerExit(Collider other1)
        {
            if (other1.gameObject == playerObject)
            {
                canClimb = false;
            }
        }
    private void Update()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerObject.transform.position = new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerObject.transform.position = new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }
        }
    }
}
