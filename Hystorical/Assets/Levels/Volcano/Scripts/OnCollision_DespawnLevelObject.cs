using UnityEngine;
using System.Collections;

public class OnCollision_DespawnLevelObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "LevelObject") || (col.gameObject.tag == "LevelFloor"))
		{
			//Despawn level object if it collides with this object
			Destroy(col.gameObject);
		}
	}
}
