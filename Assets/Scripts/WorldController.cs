using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
	public float mazeRotationSpeed = 50f;

	public FauxGravityAttractor attractor;
	public Light gravityLight;
	
	public KeyCode rotationToggleKey;
	public KeyCode gravityToggleKey;
	public bool flipKeys = false;
	
	void Start () {
		
	}
	
	void Update () {

		if (Input.GetKeyDown(currentKey(rotationToggleKey)))
		{
			changeRotation();
		}
		
		if (Input.GetKeyDown(currentKey(gravityToggleKey)))
		{
			changeGravity();
			gravityLight.enabled = !gravityLight.enabled;
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
