using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float MaxSpeed = 5;
	public float MaxAccel = 10;
	public float AnimationScale = 1;
	public bool Player = false;
	public float Xdiff = 0;
	
    public Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
	}

    void FixedUpdate()
    {
		Vector3 target = Vector3.zero;
		
        if (Player)
        {
            var horiz = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");

            target = new Vector3(horiz, 0, vert);
        }
        else
        {
			var centerX = GameObject.Find("WorldCenterMarker").transform.localPosition.x;
        	var selfX = transform.localPosition.x;
			Xdiff = centerX - selfX;
			
			target = /*Quaternion.AngleAxis(Random.value * 360, Vector3.up) * rigidbody.velocity +*/ (Vector3.right*Xdiff);
		}
		
		Vector3 targetNormalised = Vector3.ClampMagnitude(target, 1) * MaxSpeed;
		
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

		_animator.SetFloat("speed", target.magnitude * AnimationScale);
    }

	// Update is called once per frame
	void Update () {
	}
}
