using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public Transform[] checkpoints;
	public float speed;
	public float range_around_checkpoint;

	public int curr_dest = 0;
	public bool backwards;
	public int prev_checkpoint;

	private Transform myTransform;
	private Rigidbody2D myRigidbody2D;
	public SpriteRenderer MySpriteRenderer;
	
	void Start ()
	{
		myTransform = GetComponent<Transform>();
	}
	
	void FixedUpdate ()
	{
		if (!closeToCheckpoint(transform.position, checkpoints[curr_dest].position))
		{
			Vector2 pos = Vector3.MoveTowards(transform.position, checkpoints[curr_dest].position, speed * Time.fixedDeltaTime);
			myTransform.position = pos;
			
			
			// Change animation direction if needed
//			Vector2 toCenter = new Vector3(0, 0, 0) - transform.position;
//			Vector2 toCheckpoint = checkpoints[curr_dest].position - transform.position;
//			
//			float angle = Vector2.Angle(toCenter, toCheckpoint);
//			Vector3 cross = Vector3.Cross(toCheckpoint, toCenter);
//			if (cross.z > 0) angle = 360 - angle;
//			
////			Debug.Log(angle);
//			if (angle > 180 && angle < 360) MySpriteRenderer.flipX = !MySpriteRenderer.flipX;
		}
		else curr_dest = nextCheckpoint(curr_dest);
	}

	
	protected bool closeToCheckpoint(Vector3 myPos, Vector3 checkpointPos)
	{
		return Vector3.Distance(myPos, checkpointPos) <= range_around_checkpoint;
	}
	
	
	protected int nextCheckpoint(int current)
	{
		if (current == 0) backwards = false;
		else if (current == checkpoints.Length - 1) backwards = true;
		
		if (backwards) return current - 1;
		return current + 1;
	}
}
