using UnityEngine;
using System.Collections;

public class CowController : MovementController {
	
	public bool IsPanicked = false;
	public int IdleTimer = -1;
	public Vector3 Goal = Vector3.zero;
		
	protected override void SetupTarget() 
	{
		Debug.Log("SetupTarget " + gameObject.name);
		
		_target = GameObject.Find("WorldCenterMarker").transform.position - gameObject.transform.position;
		//if (IdleTimer < 0) 
		//{
		//	var centerMarker = GameObject.Find("WorldCenterMarker");			
		//	Goal = new Vector3(Random.Range(-15f, 15f),0,Random.Range(-15f, 15f));
		//	_target = centerMarker.transform.position + Goal;
		//	
		//	IdleTimer = Random.Range(100, 250);
		//}
		//
		//var diffToTarget = rigidbody.position - _target;
		//if (diffToTarget.magnitude < 1) 
		//{
		//	IdleTimer--;
		//}
	}
}
