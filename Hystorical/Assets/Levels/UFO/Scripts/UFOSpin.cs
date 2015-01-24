using UnityEngine;
using System.Collections;

public class UFOSpin : MonoBehaviour {

	public float UFOSpinRate = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles += new Vector3(0, UFOSpinRate * Time.deltaTime, 0);
	}
}
