using UnityEngine;
using System.Collections;

public class RockCollisionSoundHandler : MonoBehaviour 
{
	public AudioClip[] collisionSounds;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame 	
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			audio.clip = collisionSounds[3];
			audio.pitch = Random.Range(0.7f, 1.3f);
			audio.Play();
		}
		
		else
		{
			audio.clip = collisionSounds[(int)Random.Range(0,2)];
			audio.pitch = Random.Range(0.7f, 1.3f);
			audio.Play();
		}
	}
}