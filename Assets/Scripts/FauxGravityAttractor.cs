using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

	public float SPEED = 50f;
	public float GRAVITY = -9.8f;
	public const float MAX_GRAVITY = -9.8f;
	public const float MIN_GRAVITY = 2f;
	
	
	public void Attract(Transform body, bool gravity_on = true, bool rotation_on = true)
	/**
	 * This should be called by the FauxGravityBody so it will be pulled to the attractor.
	 * Use the boolean variables to control if the body will be affected by gravity\rotation of the attractor.
	 */
	{
		Vector2 gravityUp = (body.position - transform.position).normalized;
		Vector2 bodyUp = body.up;
		
		if (gravity_on) body.GetComponent<Rigidbody2D>().AddForce(gravityUp * GRAVITY);
		if (rotation_on)
		{
			Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
			body.rotation = Quaternion.Slerp(body.rotation, targetRotation, SPEED * Time.deltaTime);
		}
	}
	
	
	public void changeGravityDirection()
	/**
	 * Changes the gravity direction in\out.
	 */
	{
		if (compare(GRAVITY, MAX_GRAVITY))
		{
			GRAVITY = MIN_GRAVITY;
		}
		else
		{
			GRAVITY = MAX_GRAVITY;
		}
	}


	private bool compare(float x, float y)
	/**
	 * Compares floats to a epsilon precision.
	 */
	{
		float tolerance = 1E-07f;
		return Math.Abs(x - y) < tolerance;
	}
}
