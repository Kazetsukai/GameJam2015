﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Death : Photon.MonoBehaviour {
	
	public float DieTimer = -1;
	
	public AudioClip[] DeathSounds;
	
	public GameObject Ragdoll;
	public GameObject CharacterAvatarObject;
	
	private Dictionary<string, Vector3> _velocities = new Dictionary<string, Vector3>();
	private Dictionary<string, Vector3> _lastPos = new Dictionary<string, Vector3>();
	private Dictionary<string, Quaternion> _angularVelocities = new Dictionary<string, Quaternion>();
	private Dictionary<string, Vector3> _lastForward = new Dictionary<string, Vector3>();

	public GameObject particleElectric;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	void FixedUpdate () 
	{		
		UpdateVelocities(CharacterAvatarObject.transform);
        
		if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			DieByFire(3,5);
		}
        
        if (DieTimer >= 0) 
		{
			DieTimer--;
		}
		
		if (DieTimer == 0) 
		{
			Die();
		}
	}
	
	public void DieSound()
	{
		var deathsound = GetComponent<AudioSource>();
		if (DeathSounds.Length > 0) 
		{
			deathsound.clip = DeathSounds[Random.Range(0,DeathSounds.Length)];
			deathsound.Play();
		}
	}

	public void DieByFire(int minLifeS, int maxLifeS)
	{
		DieSound();
		
		var fireEmitter = transform.FindChild("Fire");
		if (fireEmitter != null) 
		{
			fireEmitter.gameObject.SetActive(true);			
			DieTimer = Random.Range(minLifeS*50, maxLifeS*50);
		}
		
		var controller = GetComponent<PersonController>();
		if (controller != null) 
		{
			controller.IsPanicked = true;
		}
	}

	public void DieByElectricity()
	{
		Instantiate(particleElectric, this.transform.position, new Quaternion());
		DieSound();
		Die();
	}

	public void Die()
	{		

		GetComponent<CapsuleCollider>().enabled = false;
		
		rigidbody.useGravity = false;
		rigidbody.freezeRotation = false;
		rigidbody.velocity = Vector3.zero;
        
		Ragdoll.SetActive(true);
		
		// Set all bones to same position as animated character
		CopyPosition(CharacterAvatarObject.transform, Ragdoll.transform);
		
		var fireEmitter = transform.FindChild("Fire");
		if (fireEmitter != null) 
		{
			fireEmitter.SetParent(Ragdoll.transform.FindChild("Armature"));
		}
		
		CharacterAvatarObject.SetActive(false);
		
		foreach (var behaviour in GetComponents<MonoBehaviour>()) {
			behaviour.enabled = false;
        }
	}
	
	void UpdateVelocities (Transform transform)
	{
		string name = transform.gameObject.name;
		
		if (!_velocities.ContainsKey(name))
		{
			_velocities.Add(name, Vector3.zero);
			_lastPos.Add (name, transform.position);
			_lastForward.Add (name, transform.forward);
			_angularVelocities.Add(name, Quaternion.identity);
		}
		else
		{
			var lastPos = _lastPos[name];
			var lastForward = _lastForward[name];
			_velocities[name] = transform.position - lastPos;
			_angularVelocities[name] = Quaternion.FromToRotation(lastForward, transform.forward);
			_lastPos[name] = transform.position;
			_lastForward[name] = transform.forward;
        }
        
        foreach (Transform t in transform)
        {
            UpdateVelocities(t);
        }
    }
    
    public void CopyPosition(Transform fromTransform, Transform toTransform)
	{ 
		toTransform.position = fromTransform.position;
		toTransform.rotation = fromTransform.rotation;
		
		if (toTransform.rigidbody != null)
		{
			if (_velocities.ContainsKey(toTransform.name))
			{
				var velocity = _velocities[toTransform.name] * (1.0f/Time.fixedDeltaTime);
				toTransform.rigidbody.AddForce(velocity, ForceMode.VelocityChange);
				
				var angVel = _angularVelocities[toTransform.name];
				angVel.x *= (1.0f/Time.fixedDeltaTime);
				angVel.y *= (1.0f/Time.fixedDeltaTime);
				angVel.z *= (1.0f/Time.fixedDeltaTime);
				angVel.w *= (1.0f/Time.fixedDeltaTime);
				
				toTransform.rigidbody.AddTorque(angVel.x, angVel.y, angVel.z, ForceMode.VelocityChange);
			}
		}
		
		foreach (Transform child in fromTransform)
        {
            foreach (Transform ragChild in toTransform)
            {
                if (ragChild.gameObject.name == child.gameObject.name)
                {
                    CopyPosition (child, ragChild);
                }
            }
        }
    }
}
