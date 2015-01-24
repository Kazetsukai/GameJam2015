using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UFOController : MovementController {
	
	private IEnumerable<GameObject> targets;
	public GameObject currentTarget = null;
	
	protected override void Init()
	{
		var allObj = GameObject.FindObjectsOfType<GameObject>();
		
		targets = allObj.Where(o=>o.layer == LayerMask.NameToLayer("Bounded"));
	}
	
	protected override void SetupTarget()
	{
		if (currentTarget == null) 
		{
			var people = targets.Where(t=>t.name == "Player" || t.name == "NPC");
			int index = Random.Range(0, people.Count());
			currentTarget = people.ElementAt(index);
		}
		
		var centerMarker = GameObject.Find("WorldCenterMarker");
		
		_target = (currentTarget.transform.position) - transform.position;
	}
	
	void OnTriggerEnter(Collider collider) 
	{
		if (currentTarget == null || currentTarget.name != "Cow") 
		{
			currentTarget = collider.gameObject;
		}
	}
}
