using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    float speed;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, 2);
        speed = 15;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += transform.forward * Time.deltaTime * speed;
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Hit(10);
            Debug.Log("Hit enemy");
            Destroy(this.gameObject);
        }
    }

}
