using UnityEngine;
using System.Collections;

public class PlayBurnSoundWhenTouchLava : MonoBehaviour 
{
	public ParticleSystem smokeParticles;

	bool burning;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (burning)
		{
			if (this.renderer.material.color != Color.black)
			{
				this.renderer.material.color -= new Color(0.01f,0.01f,0.01f);
			}
		}	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Lava")
		{
			if(audio != null)
			{
			this.gameObject.audio.Play();
			}
		}

		Instantiate(smokeParticles, this.transform.position,new Quaternion());
		burning = true;

	}
}
