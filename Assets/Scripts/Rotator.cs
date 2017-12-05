using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public KeyCode changeDirectionKey;
	public bool changeDirection;
	public float mazeRotationSpeed = 50f;
	private bool isSlowingDown = false;
	public float slowingFactor = 0.9f;
		
	void Start () {
		
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
		if (isSlowingDown)
		{
			if (!FauxGravityAttractor.compare(mazeRotationSpeed, 0f)) mazeRotationSpeed *= slowingFactor;
		}
	}


	void changeRotation()
	{
		mazeRotationSpeed = -mazeRotationSpeed;
	}

	public void stopRotation()
	{
		isSlowingDown = true;
	}
	
}
