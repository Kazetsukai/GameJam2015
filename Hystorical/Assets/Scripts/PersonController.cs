using UnityEngine;
using System.Collections;

public class PersonController : MovementController {
	
	public bool IsPanicked = false;
	public int PanicDirection = -1;
	public int PanicTimer = 0;
	
	public bool Player = false;
	
	public float NPCFollowFactor = 1f;
	public Vector3 NPCGoal = Vector3.zero;
	public float NPCIntelligence = 0;
    private int NPCChangeMind = 0;
    
    protected override void SetupTarget() 
	{
		if (IsPanicked) 
		{
			PanicTimer--;
			if (PanicTimer < 0) {
				PanicDirection = Random.Range(-45,45);
				PanicTimer = Random.Range(30,50);
			}
			_target = Quaternion.AngleAxis(PanicDirection, Vector3.up) * Vector3.right;
		}
        else if (Player)
        {
            var horiz = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");

            _target = new Vector3(horiz, 0, vert);
        }
        else
        {
        	if (NPCChangeMind <= 0) {
				NPCGoal = new Vector3(Random.Range(-15f + 20f * NPCIntelligence, 15f),0,Random.Range(-15f,15f));
        		NPCChangeMind = Random.Range(40,400);
			}
			
			NPCChangeMind--;

            var centerMarker = GameObject.Find("WorldCenterMarker");
            if (centerMarker != null)
            {
                var goal = centerMarker.transform.position + NPCGoal;
                var self = rigidbody.transform.position;
                var posDiff = goal - self;

                _target = (Quaternion.AngleAxis(Random.value * 360, Vector3.up) * Vector3.right) + (posDiff * NPCFollowFactor);
            }
		}
	}

    [RPC]
    void SetRemote()
    {
        Remote = true;
        Debug.Log("Set player to remote player");
    }
}
