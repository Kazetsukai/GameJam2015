using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public Animator _animator;
	
	public float MaxSpeed = 5;
	public float MaxAccel = 10;
	
	protected Vector3 _target = Vector3.zero;
	
	public bool Remote = false;

	void Start () {
		_animator = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate()
	{
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
