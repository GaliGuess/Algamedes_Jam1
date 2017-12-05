using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public KeyCode changeDirectionKey;
	public bool changeDirection;
	public float mazeRotationSpeed = 50f;
		
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
	}


	void changeRotation()
	{
		mazeRotationSpeed = -mazeRotationSpeed;
	}
	
}
