using UnityEngine;
using System.Collections;

public class Abduct : MonoBehaviour {

	void OnTriggerEnter(Collider collider) 
	{
		var killer = collider.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByElectricity();
		}
	}
}
