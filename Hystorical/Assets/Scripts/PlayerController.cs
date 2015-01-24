using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public Animator _animator;
	
    public float MaxSpeed = 5;
	public float MaxAccel = 10;
	
	public bool IsPanicked = false;
	public Vector3 PanicDirection = Vector3.zero;
	public int PanicTimer = 0;
	
	public bool Player = false;
	
	public float NPCFollowFactor = 1f;
	public Vector3 NPCGoal = Vector3.zero;
	public int NPCIntelligence = 0;
    private int NPCChangeMind = 0;

    Vector3 _target = Vector3.zero;

    public bool Remote = false;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
		//PanicDirection = Random.rotation
    }
    
    void FixedUpdate()
    {
		if (IsPanicked) 
		{
			//Vector3.RotateTowards(rigidbody.velocity, PanicDirection,
			
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

        Vector3 targetNormalised = Vector3.ClampMagnitude(_target, 1) * MaxSpeed;
		
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

        _animator.SetFloat("speed", _target.magnitude);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Vector3 pos = transform.localPosition;
        stream.Serialize(ref pos);
        stream.Serialize(ref _target);
        transform.localPosition = pos;
    }

    [RPC]
    void SetRemote()
    {
        Remote = true;
    }
}
