using UnityEngine;
using System.Collections;

public class LevelSpawner : Photon.MonoBehaviour {

    public float TimeBetweenLevels = 20;

    float _levelTimer;
    

	// Use this for initialization
	void Start () {
        _levelTimer = TimeBetweenLevels;
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if (_levelTimer >= TimeBetweenLevels)
            {
                photonView.RPC("ChangeLevel", PhotonTargets.All, new object[] { "LevelVolcano", 20 });

                _levelTimer = 0;
            }

            _levelTimer += Time.deltaTime;
        }
	}

    [RPC]
    void ChangeLevel(string levelName, int randomSeed)
    {
        Debug.Log("WOOP " + levelName);


    }
}
