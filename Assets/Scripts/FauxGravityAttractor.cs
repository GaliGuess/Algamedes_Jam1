using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

/**
 * Attracts FauxGravityBody objects to it's center simulating gravity.
 */
public class FauxGravityAttractor : MonoBehaviour {

	public float SPEED = 50f;
	public float GRAVITY = -9.8f;
	public const float MAX_GRAVITY = -9.8f;
	public const float MIN_GRAVITY = 2f;
	
	
	/**
	 * This should be called by the FauxGravityBody so it will be pulled to the attractor.
	 * Use the boolean variables to control if the body will be affected by gravity\rotation of the attractor.
	 */
	public void Attract(Transform body, bool gravity_on = true, bool rotation_on = true)
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
	
	/**
	 * Changes the gravity direction in\out.
	 */
	public void changeGravityDirection()
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

	/**
	 * Compares floats to a epsilon precision.
	 */
	public static bool compare(float x, float y)
	{
		float tolerance = 1E-07f;
		return Math.Abs(x - y) < tolerance;
	}
}
