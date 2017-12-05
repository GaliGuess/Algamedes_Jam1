using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int health = 3;
	public bool invulnerable = false;

	// Spider web related
	public int WebTurnsLeft = 0;
	private GameObject web;
	public int MaxTurnsInWeb = 100;
	private int toWebSpeed = 4;

	private Rigidbody2D rigidbody2d;
	public SpriteRenderer playerSpriteRenderer;
	public Rotator maze; // used to set the sprite's direction

	// Used to animate the player when hurt
	private Animator animator;
	public bool hurt;
	public int turnsToHurt;
	public int turnsLeftToHurt = 0;
	public string hurtAnimBoolParamName;
	private int hurtAnimBoolParamId;
	
	public Light playerLight;
	public float lightReductionFactor = 0.9f;
	
	
	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		hurtAnimBoolParamId = Animator.StringToHash(hurtAnimBoolParamName);
		hurt = false;
		turnsLeftToHurt = 0;
		animator.SetBool(hurtAnimBoolParamId, hurt);
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

		if (turnsLeftToHurt > 0) turnsLeftToHurt -= 1;
		if (turnsLeftToHurt == 0) hurt = false;
		animator.SetBool(hurtAnimBoolParamId, hurt);
	}

	/**
	 * Reduces the player's health
	 */
	protected void reduceHealth()
	{
		if (invulnerable) return;
		health -= 1;
		playerLight.range *= lightReductionFactor;
		hurt = true;
		turnsLeftToHurt = turnsToHurt;
//		animator.SetBool(hurtAnimBoolParamId, hurt);
	}

	/***
	 * Flips the player's sprite renderer on x axis
	 */
	public void flipPlayer()
	{
		playerSpriteRenderer.flipX = !playerSpriteRenderer.flipX;
	}
	

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			reduceHealth();
			if (health == 0) endGame(other.gameObject);
//			else turnRed();
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
	
	/**
	 * Things to do at the end of the game.
	 */
	public void endGame(GameObject killer)
	{
		EnemyScript enemy = killer.GetComponent<EnemyScript>();
		transform.position = (killer.transform.position - transform.position) / 2;
		enemy.kill();
		Debug.Log("No more health");
		maze.stopRotation();
		GetComponentInParent<WorldController>().deactivateControls();
		Destroy(gameObject);
		// end the game
	}
}
