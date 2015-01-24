using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}
	
	void OnTriggerEnter(Collider collider) 
	{
		var killer = collider.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByFire();
		}
	}
}
