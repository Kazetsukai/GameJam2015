using UnityEngine;
using System.Collections;

public class MovementController : Photon.MonoBehaviour {

	private Animator _animator = null;
	
	public float MaxSpeed;
	public float MaxAccel;
	
	public bool LookForwards = true;
	
	public float NormalYPos = 0;
	
	public Vector3 _target = Vector3.zero;
	
	public bool Remote = false;

	void Start () {
		_animator = GetComponentInChildren<Animator>();
		Init();
	}
	
	protected virtual void Init()
	{
		
	}
	
	void FixedUpdate()
	{
        if (photonView.isMine || !PhotonNetwork.connected)
		{
			DoLogic();
            SetupTarget();
		}

		Move();
	}
	
	protected virtual void DoLogic()
	{
        
    }
	
	protected virtual void SetupTarget()
	{
	
	}
	
	private void Move()
	{
		//normalise move
		Vector3 targetNormalised = Vector3.ClampMagnitude(_target, 1) * MaxSpeed;
		
		var diff = targetNormalised - rigidbody.velocity;
		var velocityChange = diff;
		var maxAccelThisFrame = MaxAccel * Time.fixedDeltaTime;
		
		if (velocityChange.magnitude > maxAccelThisFrame)
		{
			velocityChange = velocityChange.normalized * maxAccelThisFrame;
		}
		
		//move
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		
		//fall
        var oldpos=transform.position;
		var ydiff = Mathf.Max(-5f * Time.fixedDeltaTime, NormalYPos-oldpos.y);
		transform.position = new Vector3(oldpos.x,oldpos.y + ydiff,oldpos.z);
        
		//look forwards
		var dir = rigidbody.velocity.normalized;
		if (dir.magnitude > 0.01 && LookForwards)
			transform.localRotation = Quaternion.LookRotation(dir);
		
		//set animation speed
		if (_animator.runtimeAnimatorController != null) _animator.SetFloat("Speed", _target.magnitude);
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
        Debug.Log("Set player to remote player");
	}
}
