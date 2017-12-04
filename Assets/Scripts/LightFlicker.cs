using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
	
	public float minFlickerSpeed = 0.01f;
	public float maxFlickerSpeed = 0.1f;
	public float minLightIntensity = 0f;
	public float maxLightIntensity = 1f;

	private Light light;
	
	void Start () {
		light = GetComponent<Light>();
	}
	
	void Update ()
	{
		StartCoroutine(flicker());
	}

	IEnumerator flicker()
	{
//		light.enabled = true;
		light.intensity = Random.Range(minLightIntensity, maxLightIntensity);
		yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed ));
//		light.enabled = false;
//		yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed ));
	}
}
