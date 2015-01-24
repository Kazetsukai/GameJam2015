using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public int DieTimer = -1;
	public int yougondie = 0;
	
	// Use this for initialization
	void Start () {
		yougondie = Random.Range(5,10);
	}
	
	void FixedUpdate () {
		if (Time.time > yougondie && DieTimer < 0) 
		{
			DieByFire();
		}
		
		if (DieTimer >= 0) 
		{
			DieTimer--;
		}
		
		if (DieTimer == 0) 
		{
			Die();
		}
	}
	
	public void DieByFire()
	{
		var fireEmitter = transform.FindChild("Fire");
		if (fireEmitter != null) 
		{
			fireEmitter.gameObject.SetActive(true);
			DieTimer = Random.Range(300, 600);
		}
	}
	
	public void Die()
	{
		transform.gameObject.SetActive(false);
	}
}
