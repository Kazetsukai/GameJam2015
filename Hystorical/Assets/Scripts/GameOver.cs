using UnityEngine;
using System.Collections;
using System.Linq;

public class GameOver : MonoBehaviour {

	public Canvas gameovercanvas;

	float lobbyTimer = 5f;
	bool gameover = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int playerCount = GameObject.FindGameObjectsWithTag("Player").Where (p => p.GetComponent<Death>().enabled).Count ();
		Debug.Log("there are " + playerCount + " players alive");

		if ((playerCount <= 0) && (!gameover))
		{
			try{
				GameObject muzak = GameObject.Find("Muzak");
				muzak.GetComponent<AudioSource>().Stop();	
			}
			catch{}

			this.GetComponent<Canvas>().enabled = true;
			this.GetComponent<AudioSource>().enabled = true;

			gameover = true;
		}

		if (gameover)
		{
			lobbyTimer -= Time.deltaTime;
			if ((lobbyTimer <= 0) && (PhotonNetwork.isMasterClient))
			{
				PhotonNetwork.LoadLevel(0);
			}
		}
	}
}
