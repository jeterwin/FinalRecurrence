using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    private int health, maxHealth;
    private Image healthBar;


	// Use this for initialization
	void Start () 
    {
        health = 100;
        maxHealth = 100;
        healthBar = transform.Find("EnemyCanvas").Find("HealthBG").Find("Health").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void Hit(int damage)
    {

        health -= damage;
        healthBar.fillAmount = (float)health / (float)maxHealth;

    }

}
