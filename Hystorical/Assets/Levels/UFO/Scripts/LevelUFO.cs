using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelUFO : MonoBehaviour {
	
	public GameObject worldCenterObject;

	public GameObject cowPrefab;
	public float spawnDistance = 60f; //distance cows spawn from worldCenterObject
	
	
	// Use this for initialization
	void Start () 
	{
        if (cowPrefab != null)
        {
            // Spawn NPCs
            for (int i = 0; i < 8; i++)
            {
                var position = Random.insideUnitSphere * spawnDistance + worldCenterObject.transform.position;
                position.y = 0;

                var npc = (GameObject)Instantiate(cowPrefab, position, Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}













