using UnityEngine;
using System.Collections;

public class LightFlashOnce : MonoBehaviour {
	public float intensity = 6f;
	public float flashrate = 0.01f;

	int flashCount = 0;
	public int flashes = 10;

	// Use this for initialization
	void Start () {
		this.GetComponent<Light> ().intensity = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (this.GetComponent<Light>().intensity < intensity)
		{
			this.GetComponent<Light>().intensity += flashrate * Time.deltaTime;
		}
		else
		{
			this.GetComponent<Light>().intensity = 0;
		}

		if (this.GetComponent<Light>().intensity >= intensity)
		{
			flashCount++;
			intensity *= 0.8f;
		}

		if (flashCount >= flashes) {
			Destroy(this.gameObject);
				}
	
	}
}
