using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to control the background's opacity
 */
public class bgScript : MonoBehaviour
{
	public float opacity = 1f; 
	private SpriteRenderer spriteRenderer;
	
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
	}
}
