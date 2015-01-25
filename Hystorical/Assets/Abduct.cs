using UnityEngine;
using System.Collections;

public class Abduct : MonoBehaviour {

	void OnTriggerEnter(Collider collider) 
	{
		var killer = collider.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByElectricity();
			
			var ragdoll = killer.transform.FindChild("Ragdoll");
			
			if (ragdoll != null)
			{
				Stop(ragdoll.transform);
			}
		}
	}
	
	public void Stop(Transform t)
	{
		Debug.Log ("woop");
		if (t.rigidbody != null)
			t.rigidbody.velocity = Vector3.zero;
			
		foreach (Transform tc in t)
		{
			Stop (tc);
		}
	}
}
