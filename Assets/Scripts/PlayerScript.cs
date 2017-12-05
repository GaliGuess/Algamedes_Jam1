using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int health = 3;
	public bool invulnerable = false;

	public int WebTurnsLeft = 0;
	private GameObject web;
	public int MaxTurnsInWeb = 100;
	private int toWebSpeed = 4;

	private Rigidbody2D rigidbody2d;
	public Light playerLight;
	public float lightReductionFactor = 0.9f;

	
	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	
	void FixedUpdate () {
		if (WebTurnsLeft != 0)
		{
			WebTurnsLeft -= 1;
			Vector2 webPos = Vector2.MoveTowards(rigidbody2d.position, web.transform.position, toWebSpeed * Time.fixedDeltaTime);
			rigidbody2d.MovePosition(webPos);
		}
	}

	
	protected void reduceHealth()
	{
		if (invulnerable) return;
		health -= 1;
		playerLight.range *= lightReductionFactor;
	}

	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			reduceHealth();
		}
	}


	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Web")
		{
			WebTurnsLeft = MaxTurnsInWeb;
			web = other.gameObject;
		}
	}
}
