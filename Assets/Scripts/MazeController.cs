using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
	public float mazeRotationSpeed = 50f;

	public FauxGravityAttractor attractor;
	
	public KeyCode rotationToggleKey;
	public KeyCode gravityToggleKey;
	public bool flipKeys = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(currentKey(rotationToggleKey)))
		{
			changeRotation();
		}
		
		if (Input.GetKeyDown(currentKey(gravityToggleKey)))
		{
			changeGravity();
		}
	}
	
	
	void FixedUpdate () {
//		attractor.updateGravity();
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * mazeRotationSpeed));
	}


	void changeRotation()
	{
		mazeRotationSpeed = -mazeRotationSpeed;
	}
	

	void changeGravity()
	{
		attractor.changeGravityDirection();
	}

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
}
