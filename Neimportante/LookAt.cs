using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject @object;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        //Rotate the object after the target's location
        @object.transform.LookAt(target.transform);
    }
}
