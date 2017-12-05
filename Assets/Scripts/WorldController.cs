using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller for rotating and activating gravity
 */
public class WorldController : MonoBehaviour
{
	public float mazeRotationSpeed = 50f;

	public FauxGravityAttractor attractor; // The attractor that attracts things to the center of the world
	public Light gravityLight; // is turned on when the gravity is towards the center
	
	public KeyCode rotationToggleKey;
	public KeyCode gravityToggleKey;
	public bool flipKeys = false;
	public bool playable = true; // when off the keys don't work
	
	
	void Update () {
		if (playable)
		{
			if (Input.GetKeyDown(currentKey(rotationToggleKey))) changeRotation();

			if (Input.GetKeyDown(currentKey(gravityToggleKey)))
			{
				changeGravity();
				gravityLight.enabled = !gravityLight.enabled;
			}
		}
	}
	
	void FixedUpdate () {
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * mazeRotationSpeed));
	}

	/***
	 * changes the rotation direction
	 */
	void changeRotation()
	{
		mazeRotationSpeed = -mazeRotationSpeed;
	}
	
	/***
	 * Changes the gravity direction
	 */
	void changeGravity()
	{
		attractor.changeGravityDirection();
	}

	/***
	 * Used to allow flipping of the keys between the players
	 */
	KeyCode currentKey(KeyCode key)
	{
		if (!flipKeys)
		{
			return key;
		}
		if (key == rotationToggleKey)
		{
			return gravityToggleKey;
		}
		return rotationToggleKey;
	}

	/**
	 * deactivates the user controls
	 */
	public void deactivateControls()
	{
		playable = false;
	}
}
