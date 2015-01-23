using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float MaxSpeed = 5;
	public float MaxAccel = 10;
	public float AnimationScale = 1;
	public bool Player = false;

    public Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
	}

    void FixedUpdate()
    {
        if (Player)
        {
            var horiz = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");

            var target = Vector3.ClampMagnitude(new Vector3(horiz, 0, vert), 1) * MaxSpeed;
            var diff = target - rigidbody.velocity;
            var velocityChange = diff;
            var maxAccelThisFrame = MaxAccel * Time.fixedDeltaTime;

            if (velocityChange.magnitude > maxAccelThisFrame)
            {
                velocityChange = velocityChange.normalized * maxAccelThisFrame;
            }

            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        var dir = rigidbody.velocity.normalized;
        if (dir.magnitude > 0.01)
            transform.localRotation = Quaternion.LookRotation(dir);

        _animator.SetFloat("speed", rigidbody.velocity.magnitude * AnimationScale);
    }

	// Update is called once per frame
	void Update () {
	}
}
