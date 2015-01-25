using UnityEngine;
using System.Collections;

public class CowSoundHandler : MonoBehaviour {

	public AudioClip[] cowSounds;

	public float mooRate = 1f;
	private float mooTimer = 0;

	float nextMooTime;

	// Use this for initialization
	void Start () {
		nextMooTime = Random.Range(5f, 15f);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		mooTimer += mooRate * Time.fixedDeltaTime;		

		if (mooTimer >= nextMooTime)
		{
			audio.PlayOneShot(cowSounds[Random.Range(0, cowSounds.Length - 1)]);
			mooTimer = 0;
			nextMooTime = Random.Range(5f, 15f);
		}
	}
}
