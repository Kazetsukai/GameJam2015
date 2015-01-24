using UnityEngine;
using System.Collections;

public class MovementController : Photon.MonoBehaviour {

	private Animator _animator;
	
	public float MaxSpeed;
	public float MaxAccel;
	
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
		    SetupTarget();

		Move();
	}
	
	protected virtual void SetupTarget()
	{
	
	}
	
	private void Move()
	{		
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
		
		_animator.SetFloat("Speed", _target.magnitude);
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
