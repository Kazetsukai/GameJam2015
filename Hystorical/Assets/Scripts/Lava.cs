﻿using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	
	void OnTriggerEnter(Collider collider) 
	{
		var killer = collider.gameObject.GetComponent<Death>();
		if (killer != null) 
		{
			killer.DieByFire(0, 1);
		}
	}
}
