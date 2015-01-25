using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UFOController : MovementController {
	
	private IEnumerable<GameObject> targets;
	public GameObject currentTarget = null;
	public float TractorFactor = 1;
	public float TractorRange = 2;
	
	protected override void Init()
	{
		
		LookForwards = false;
	}
	
	protected override void DoLogic()
    {
        var allObj = GameObject.FindObjectsOfType<GameObject>();

        targets = allObj.Where(o => o.layer == LayerMask.NameToLayer("Bounded"));

		if (currentTarget != null && !currentTarget.GetComponent<Death>().enabled) 
		{
			currentTarget = null;
        }
        
        if (currentTarget == null) 
		{
			var people = targets.Where(t=>(t.name.StartsWith("Player") || t.name.StartsWith("NPC")) && t.GetComponent<Death>().enabled);
			if (!people.Any()) {
				return;
			}
			int index = Random.Range(0, people.Count());
			currentTarget = people.ElementAt(index);
		}
        
        var xzPosTarget = currentTarget.transform.position;		
		var xzPos = transform.position;
		xzPosTarget.y=0;
		xzPos.y=0;
		
		if ((xzPos - xzPosTarget).magnitude < TractorRange) 
		{
			var oldpos=currentTarget.transform.position;
			currentTarget.transform.position = new Vector3(oldpos.x,oldpos.y + 10f * Time.fixedDeltaTime,oldpos.z);
			currentTarget.rigidbody.AddForce((xzPos - xzPosTarget).normalized * TractorFactor, ForceMode.VelocityChange);
		}
    }
    
    protected override void SetupTarget()
	{		
		var centerMarker = GameObject.Find("WorldCenterMarker");
		_target = Vector3.zero;
		
		if (currentTarget != null)
			_target = (currentTarget.transform.position) - transform.position;
	}
	
	void OnTriggerEnter(Collider collider) 
	{
		if (currentTarget == null || !currentTarget.name.StartsWith("Cow")) 
		{
			if (collider.gameObject.name.StartsWith("Cow")) 
			{
				currentTarget = collider.gameObject;
			}
		}
	}
}
