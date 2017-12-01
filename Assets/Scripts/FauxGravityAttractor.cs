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
	public float GRAVITY_INC = (-MAX_GRAVITY + MIN_GRAVITY) / 20;
	
	public bool gravityIncreasing = false;
	public bool gravityDecreasing = false;
	
	
	public void Attract(Transform body)
	{
		Vector2 gravityUp = (body.position - transform.position).normalized;
		Vector2 bodyUp = body.up;
		
		body.GetComponent<Rigidbody2D>().AddForce(gravityUp * GRAVITY);
		
		Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, SPEED * Time.deltaTime);
	}
	
	
	public void updateGravity()
	{
		if (gravityIncreasing)
		{
			if (GRAVITY > MAX_GRAVITY)
			{
				GRAVITY -= GRAVITY_INC;				
			}
			else
			{
				gravityIncreasing = false;
			}
			
		}
		else if (gravityDecreasing)
		{
			if (GRAVITY < MIN_GRAVITY)
			{
				GRAVITY += GRAVITY_INC;				
			}
			else
			{
				gravityDecreasing = false;
			}
		}
	}

	
	public void changeGravityDirection()
	{
		if (gravityDecreasing)
		{
			gravityIncreasing = true;
			gravityDecreasing = false;
		}
		else if (gravityIncreasing)
		{
			gravityIncreasing = false;
			gravityDecreasing = true;
		}
		else
		{
			if (compare(GRAVITY, MAX_GRAVITY))
			{
				gravityDecreasing = true;
			}
			else
			{
				gravityIncreasing = true;
			}
		}
	}
	
	
	public void changeGravityDirection2()
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
	{
		float tolerance = 1E-07f;
		return Math.Abs(x - y) < tolerance;
	}
}
