using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to fade the screen
 */
public class FadeScreen : MonoBehaviour
{
	public float startOpacity = 0f;
	public float endOpacity = 1f;
	public float opacity;
	public float opacityDiff = 0.01f;
	private Color mainColor;
	private SpriteRenderer spriteRenderer;

	private bool isFading;
	
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(0f, 0f, 0f, startOpacity);
		isFading = false;
		opacity = startOpacity;
	}

	private void FixedUpdate()
	{
		if (isFading && opacity < endOpacity) opacity += opacityDiff;
		spriteRenderer.color = new Color(mainColor.r, mainColor.g, mainColor.b, opacity);
	}

	/**
	 * Sets the fade color
	 */
	public void setColor(Color color)
	{
		mainColor = color;
		spriteRenderer.color = new Color(mainColor.r, mainColor.g, mainColor.b, startOpacity);
	}
	
	/**
	 * Starts the fading
	 */
	public void fade()
	{
		isFading = true;
	}
}
