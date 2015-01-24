using UnityEngine;
using System.Collections;

public class CowController : MovementController {
	
	public bool IsPanicked = false;
	public int IdleTimer = 0;
		
	protected override void SetupTarget() 
	{
		if (IdleTimer < 0) 
		{
			var centerMarker = GameObject.Find("WorldCenterMarker");			
			var goal = new Vector3(Random.Range(-15f, 15f),0,Random.Range(-15f, 15f));
			_target = centerMarker.transform.position + goal;
			
			IdleTimer = Random.Range(100, 250);
		}
		
		var diffToTarget = rigidbody.position - _target;
		if (diffToTarget.magnitude < 1) 
		{
			IdleTimer--;
		}
	}
}
