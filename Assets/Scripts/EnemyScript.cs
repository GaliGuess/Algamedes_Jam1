using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	private Transform myTransform;
	public SpriteRenderer mySpriteRenderer;
	private Animator animator;
	
	// Used for the enemies routine path
	public Transform[] checkpoints;
	public float speed;
	public float range_around_checkpoint;
	public int curr_dest = 0;
	public bool backwards;
	private int prev_checkpoint;
	
	// Used for the killing the player animation
	public bool attack = false;
	public string killAnimBoolParamName;
	private int killAnimBoolParamId;

	
	void Start ()
	{
		myTransform = GetComponent<Transform>();
		animator = GetComponentInChildren<Animator>();
		killAnimBoolParamId = Animator.StringToHash(killAnimBoolParamName);
		animator.SetBool(killAnimBoolParamId, attack);
	}
	
	void FixedUpdate()
	{
		if (!attack) moveToCheckpoint();
	}

	
	// to be called when the enemy kills the player
	public void kill()
	{
		attack = true;
		animator.SetBool(killAnimBoolParamId, attack);
		GetComponentInChildren<BoxCollider2D>().transform.localScale *= 2f;
	}

	/**
	 * Controls the enemy's routine path
	 */
	protected void moveToCheckpoint()
	{
		if (!closeToCheckpoint(transform.position, checkpoints[curr_dest].position))
		{
			Vector2 pos = Vector3.MoveTowards(transform.position, checkpoints[curr_dest].position, speed * Time.fixedDeltaTime);
			myTransform.position = pos;
			changeAnimDirection();
		}
		else
		{
			prev_checkpoint = curr_dest;
			curr_dest = nextCheckpoint(curr_dest);
		}
	}
	
	/**
	 * Returns true iff the given vectors' distance is smaller than the defined range.
	 */
	protected bool closeToCheckpoint(Vector3 myPos, Vector3 checkpointPos)
	{
		return Vector3.Distance(myPos, checkpointPos) <= range_around_checkpoint;
	}
	
	/**
	 * Returns the next checkpoint
	 */
	protected int nextCheckpoint(int current)
	{
		if (current == 0) backwards = false;
		else if (current == checkpoints.Length - 1) backwards = true;
		
		if (backwards) return current - 1;
		return current + 1;
	}
	
	/**
	 * Flips the spriterenderer's x axis when needed
	 */
	private void changeAnimDirection()
	{
		Vector2 toNewCp = checkpoints[curr_dest].localPosition;
		Vector2 toPrevCp = checkpoints[prev_checkpoint].localPosition;
		Vector2 localDirection = toNewCp - toPrevCp;

		if (localDirection.x < 0) mySpriteRenderer.flipX = false;
		else mySpriteRenderer.flipX = true;
			
		if (myTransform.localPosition.y < 0) mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
	}
}
