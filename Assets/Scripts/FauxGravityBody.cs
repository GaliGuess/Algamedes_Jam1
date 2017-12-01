using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
	public FauxGravityAttractor attractor;
	private Rigidbody2D myRigidBody2D;
	private Transform myTransform;
	
	private float GRAVITY_OFF = 0f;
	
	// Use this for initialization
	void Start ()
	{
		myRigidBody2D = GetComponent<Rigidbody2D>();
		myRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		myRigidBody2D.gravityScale = GRAVITY_OFF;
		myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		attractor.Attract(myTransform);
	}
}
