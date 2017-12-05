using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Used to make flickering lights
 */
public class LightFlicker : MonoBehaviour {
	
	public float minFlickerSpeed = 0.01f;
	public float maxFlickerSpeed = 0.1f;
	public float minLightIntensity = 0f;
	public float maxLightIntensity = 1f;

	private Light myLight;
	
	void Start () {
		myLight = GetComponent<Light>();
	}
	
	void Update ()
	{
		StartCoroutine(flicker());
	}

	IEnumerator flicker()
	{
		myLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
		yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed ));
	}
}
