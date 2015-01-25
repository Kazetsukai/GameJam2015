using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Canvas gameovercanvas;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
		Debug.Log("there are " + playerCount + " players alive");

		if (playerCount <= 0)
		{
			try{
				GameObject muzak = GameObject.Find("Muzak");
				muzak.GetComponent<AudioSource>().Stop();	
			}
			catch{}

			gameovercanvas.transform.parent = Camera.main.transform;
			gameovercanvas.enabled = true;
		}
	}
}
