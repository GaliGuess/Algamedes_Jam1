using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
	public FauxGravityAttractor attractor;
	private Rigidbody2D myRigidBody2D;
	private Transform myTransform;
	
	public bool gravity = true;
	public bool rotation = true;
	public float speedLimit = 3.25f;
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
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		attractor.Attract(myTransform, gravity, rotation);
		if (myRigidBody2D.velocity.magnitude > speedLimit) limitSpeed_Strict();
		currentSpeed = myRigidBody2D.velocity.magnitude;
	}


	void limitSpeed_Strict()
	{
		Vector2 normalizedVelocity = myRigidBody2D.velocity.normalized;
		myRigidBody2D.velocity = normalizedVelocity * speedLimit;
	}
	
	
	void LimitSpeed_Natural()
	{
		float breakSpeed = myRigidBody2D.velocity.magnitude - 2*speedLimit;
		Vector2 normalizedVelocity = myRigidBody2D.velocity.normalized;
		Vector2 brakeForce = normalizedVelocity * breakSpeed;
		myRigidBody2D.AddForce(brakeForce);

	}
}
