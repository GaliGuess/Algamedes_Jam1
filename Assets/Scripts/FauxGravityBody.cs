using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This should be applied to every objects that needs to be attracted by the FauxBodyAttractor
 */
public class FauxGravityBody : MonoBehaviour
{
	public FauxGravityAttractor attractor;
	private Rigidbody2D myRigidBody2D;
	private Transform myTransform;
	
	public bool gravity = true; // disabling this will make the oobject not affected by the attractor's gravity
	public bool rotation = true; // disabling this will make the oobject not affected by the attractor's rotation
	public const float speedLimit = 3.25f;
	public float currentSpeed;
	private float GRAVITY_OFF = 0f;
	
	
	void Start ()
	{
		myRigidBody2D = GetComponent<Rigidbody2D>();
		
		if (!rotation)
		{
			myRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		myRigidBody2D.gravityScale = GRAVITY_OFF;
		myTransform = transform;
	}

	void FixedUpdate ()
	{
		attractor.Attract(myTransform, gravity, rotation);
		if (myRigidBody2D.velocity.magnitude > speedLimit) limitSpeed_Strict();
		currentSpeed = myRigidBody2D.velocity.magnitude;
	}

	/***
	 * Changes the body's speed to speedLimit.
	 */
	void limitSpeed_Strict()
	{
		Vector2 normalizedVelocity = myRigidBody2D.velocity.normalized;
		myRigidBody2D.velocity = normalizedVelocity * speedLimit;
	}
	
	/**
	 * Slows the body down to speedLimit by applying a breaking force.
	 */
	void LimitSpeed_Natural()
	{
		float breakSpeed = myRigidBody2D.velocity.magnitude - 2 * speedLimit;
		Vector2 normalizedVelocity = myRigidBody2D.velocity.normalized;
		Vector2 brakeForce = normalizedVelocity * breakSpeed;
		myRigidBody2D.AddForce(brakeForce);

	}
}
