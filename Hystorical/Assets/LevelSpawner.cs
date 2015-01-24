using UnityEngine;
using System.Collections;

public class LevelSpawner : Photon.MonoBehaviour {

    public GameObject LevelContainer;
    public float TimeBetweenLevels = 30;

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
        // Remove old level stuff
        foreach (Transform transform in LevelContainer.transform)
        {
            Destroy(transform.gameObject);
        }

        var volcanoLevel = Resources.Load<GameObject>("Levels/Volcano");

        if (volcanoLevel != null)
        {
            var level = (GameObject)Instantiate(volcanoLevel, Vector3.zero, Quaternion.identity);
            level.transform.SetParent(LevelContainer.transform);
        }

        // Move players to 0
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.transform.position = Vector3.zero;
        }
    }
}
