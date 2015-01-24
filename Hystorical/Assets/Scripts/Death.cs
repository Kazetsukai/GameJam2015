using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public int DieTimer = -1;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	void FixedUpdate () 
	{		
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
			DieTimer = Random.Range(60, 150);
		}
		
		var controller = GetComponent<PlayerController>();
		if (controller != null) 
		{
			controller.IsPanicked = true;
		}
	}
	
	public void Die()
	{
		//actually ragdoll
	
		transform.gameObject.SetActive(false);
	}
}
