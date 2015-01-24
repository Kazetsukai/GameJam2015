using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public Animator _animator;
	
    public float MaxSpeed = 5;
	public float MaxAccel = 10;
	
	public bool IsPanicked = false;
	public int PanicAngle = 0;
	public int PanicTimer = 0;
	
	public bool Player = false;
	
	public float NPCFollowFactor = 1f;
	public Vector3 NPCGoal = Vector3.zero;
	public int NPCIntelligence = 0;	
	private int NPCChangeMind = 0;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
    }
    
    void FixedUpdate()
    {
		Vector3 target = Vector3.zero;
		if (IsPanicked) 
		{
			target = Quaternion.AngleAxis(PanicAngle, Vector3.up) * Vector3.right;
			
			if (PanicTimer < 0) 
			{
				PanicAngle = Random.Range(-45,45);
				PanicTimer = Random.Range(20,40);
			}
			PanicTimer--;
		}
        else if (Player)
        {
            var horiz = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");

            target = new Vector3(horiz, 0, vert);
        }
        else
        {
        	if (NPCChangeMind <= 0) 
        	{
				NPCGoal = new Vector3(Random.Range(-15f + 20f * NPCIntelligence, 15f),0,Random.Range(-15f,15f));
        		NPCChangeMind = Random.Range(40,400);
			}
			
			NPCChangeMind--;
        	
			var goal = GameObject.Find("WorldCenterMarker").transform.position + NPCGoal;
        	var self = rigidbody.transform.position;
			var posDiff = goal - self;
			
			target = (Quaternion.AngleAxis(Random.value * 360, Vector3.up) * Vector3.right) + (posDiff * NPCFollowFactor);
		}
		
		Vector3 targetNormalised = Vector3.ClampMagnitude(target, 1) * MaxSpeed * (IsPanicked ? 1.5f : 1);
		
		var diff = targetNormalised - rigidbody.velocity;
		var velocityChange = diff;
		var maxAccelThisFrame = MaxAccel * Time.fixedDeltaTime;
		
		if (velocityChange.magnitude > maxAccelThisFrame)
		{
			velocityChange = velocityChange.normalized * maxAccelThisFrame;
		}
		
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        var dir = rigidbody.velocity.normalized;
        if (dir.magnitude > 0.01)
            transform.localRotation = Quaternion.LookRotation(dir);

		_animator.SetFloat("speed", target.magnitude);
    }
}
