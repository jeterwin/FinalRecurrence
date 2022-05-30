using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
	}

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
