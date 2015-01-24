using UnityEngine;
using System.Collections;

public class Flaming : MonoBehaviour {
	
	void OnTriggerEnter(Collider collider) 
	{
		var killer = collider.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByFire(3, 5);
		}
	}
}
