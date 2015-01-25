using UnityEngine;
using System.Collections;

public class LevelSpawner : Photon.MonoBehaviour {

    public GameObject LevelContainer;
    public float TimeBetweenLevels = 30;

    float _levelTimer;

    static string[] Levels = new string[] {
        "Levels/Volcano",
        "Levels/UFO",
    };

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
                var level = Levels[Random.Range(0, Levels.Length - 1)];

                photonView.RPC("ChangeLevel", PhotonTargets.All, level);

                _levelTimer = 0;
            }

            _levelTimer += Time.deltaTime;
        }
	}

    [RPC]
    void ChangeLevel(string levelName)
    {
        // Remove old level stuff
        foreach (Transform transform in LevelContainer.transform)
        {
            Destroy(transform.gameObject);
        }

        var volcanoLevel = Resources.Load<GameObject>(levelName);

        if (volcanoLevel != null)
        {
            var level = (GameObject)Instantiate(volcanoLevel, Vector3.zero, Quaternion.identity);
            level.transform.SetParent(LevelContainer.transform);
        }

        // Move players to 0
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var position = Random.insideUnitSphere * 10;
            position.y = 0;
        }
    }
}
