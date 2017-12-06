using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int health = 3;
	public bool invulnerable = false;

	public bool endOfGame;
	public int turnsBeforeClosing = 150;
	public int turnsLeftToClose = 0;

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
		endOfGame = false;
	}
	
	
	void FixedUpdate () {
		if (endOfGame)
		{
			if (turnsLeftToClose == 0) Application.Quit();
			else turnsLeftToClose -= 1;
		}
		else
		{
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
			Debug.Log("Stung by wasp");
			reduceHealth();
			if (health == 0) loseGame(other.gameObject);
		}
	}


	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Web")
		{
			Debug.Log("Caught in web");
			other.gameObject.GetComponent<WebAnim>().activateAnim();
			WebTurnsLeft = MaxTurnsInWeb;
			web = other.gameObject;
		}
		if (other.gameObject.tag == "Winner")
		{
			Debug.Log("You've Won!!");
			endGame();
		}
	}
	
	/**
	 * Things to do at the end of the game.
	 */
	public void loseGame(GameObject killer)
	{
		EnemyScript enemy = killer.GetComponent<EnemyScript>();
		transform.position = (killer.transform.position - transform.position) / 2;
		playerLight.enabled = false;
		enemy.kill();
		Debug.Log("No more health");
		maze.stopRotation();
		Destroy(playerSpriteRenderer);
//		Destroy(gameObject);
		endGame();
	}

	/**
	 * ends the game.
	 */
	private void endGame()
	{
		Debug.Log("Bye bye...");
		GetComponentInParent<WorldController>().deactivateControls();
		endOfGame = true;
		turnsLeftToClose = turnsBeforeClosing;
	}
}
