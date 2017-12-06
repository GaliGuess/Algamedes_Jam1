using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAnim : MonoBehaviour
{
	private Animator animator;
	public bool caught;
	public int animDuration = 30;
	public int turnsLeft = 0;
	public string caughtAnimBoolParamName;
	private int caughtAnimBoolParamId;
	
	void Start ()
	{
		animator = GetComponentInChildren<Animator>();
		caught = false;
		caughtAnimBoolParamId = Animator.StringToHash(caughtAnimBoolParamName);
		animator.SetBool(caughtAnimBoolParamId, caught);
	}
	
	void Update () {
		animator.SetBool(caughtAnimBoolParamId, caught);
		if (turnsLeft == 0) caught = false;
		else turnsLeft -= 1;
	}

	public void activateAnim()
	{
		caught = true;
		animator.SetBool(caughtAnimBoolParamId, caught);
		turnsLeft = animDuration;
	}
}
