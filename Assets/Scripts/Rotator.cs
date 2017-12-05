using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Simplified controller for rotating an object. 
 */
public class Rotator : MonoBehaviour
{
	public KeyCode changeDirectionKey;
	public float mazeRotationSpeed = 50f;
	public bool changeDirection;
	private bool isSlowingDown;
	
	void Start ()
	{
		isSlowingDown = false;
	}
	
	void Update () {	
		if (changeDirection || Input.GetKeyDown(changeDirectionKey))
		{
			changeRotation();
			changeDirection = false;
		}
	}
	
	void FixedUpdate () {
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * mazeRotationSpeed));
		if (isSlowingDown && mazeRotationSpeed > 0) mazeRotationSpeed -= 0.5f;
	}

	/**
	 * Changes the direction of rotation.
	 */
	void changeRotation()
	{
		mazeRotationSpeed = -mazeRotationSpeed;
	}

	/**
	 * Stops the rotation (gradually until zero)
	 */
	public void stopRotation()
	{
		isSlowingDown = true;
	}
	
}
