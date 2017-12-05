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

	public Rotator maze;
	public SpriteRenderer playerSpriteRenderer;
	
	
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
		if (maze.mazeRotationSpeed > 0) playerSpriteRenderer.flipX = true;
		else playerSpriteRenderer.flipX = false;
	}

	
	protected void reduceHealth()
	{
		if (invulnerable) return;
		health -= 1;
		playerLight.range *= lightReductionFactor;
	}


	public void flipPlayer()
	{
		playerSpriteRenderer.flipX = !playerSpriteRenderer.flipX;
	}


	protected void turnRed()
	{
//		while (turnsToRed > 0)
//		{
//			playerSpriteRenderer.color()
//		}
		
	}
	

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			reduceHealth();
			if (health == 0) endGame(other.gameObject);
		}
	}


	public void endGame(GameObject killer)
	{
		EnemyScript enemy = killer.GetComponent<EnemyScript>();
		transform.position = (killer.transform.position - transform.position) / 2;
//		rigidbody2d.MovePosition(killer.transform.position / 2);
		enemy.kill();
		Debug.Log("No more health");
		maze.stopRotation();
		Destroy(gameObject);
		// end the game
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
