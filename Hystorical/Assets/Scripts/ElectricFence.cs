using UnityEngine;
using System.Collections;

public class ElectricFence : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) 
	{
		var killer = col.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByElectricity();
		}
	}
}
