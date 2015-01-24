using UnityEngine;
using System.Collections;

public class IgnoreCollisionWith : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Living") 
		{
			Physics.IgnoreCollision(col.collider, this.collider);
		}
	}
}
