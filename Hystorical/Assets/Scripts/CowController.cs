using UnityEngine;
using System.Collections;

public class CowController : MovementController {
	
	public bool IsPanicked = false;
	public int IdleTimer = -1;
	public Vector3 Goal = Vector3.zero;
		
	protected override void SetupTarget() 
	{
		var centerMarker = GameObject.Find("WorldCenterMarker");
		
		if (IdleTimer < 0) 
		{		
			Goal = new Vector3(Random.Range(-14f, 14f),0,Random.Range(-14f, 14f));
			
			IdleTimer = Random.Range(100, 250);
		}
		
		var diffToTarget = rigidbody.position - (centerMarker.transform.position + Goal);
		if (diffToTarget.magnitude < 1) 
		{
			IdleTimer--;
		}
		
		var target = (centerMarker.transform.position + Goal) - transform.position;
		
		_target = target.magnitude < 1 ? Vector3.zero : target;
	}
}
