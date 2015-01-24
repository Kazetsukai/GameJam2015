using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelUFO : MonoBehaviour {
	
	public GameObject worldCenterObject;
	private GameObject LevelObjectsParent;
	
	public GameObject floorPrefab;
	
	public float levelWidth = 30f;

	public GameObject cowPrefab;
	public float cowSpawnRate = 1f;	
	public float spawnTime = 5f;	
	public float cowSpawnTimer = 0f;

	public float spawnDistance = 60f; //distance cows spawn from worldCenterObject
	
	public int numSpawned = 0;
	
	float objectDespawnerDistance = 80f;
		
	public GameObject LevelBounds;
	
	// Use this for initialization
	void Start () 
	{
		LevelObjectsParent = new GameObject ("LevelObjects");
		LevelObjectsParent.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Update positions
		LevelBounds.transform.position = new Vector3(worldCenterObject.transform.position.x, LevelBounds.transform.position.y, LevelBounds.transform.position.z);
		
		//update timers
		cowSpawnTimer += Time.fixedDeltaTime * cowSpawnRate;
		
		//Spawn floor objects as worldCenterObject moves
		int numToSpawn = ((int)(worldCenterObject.transform.position.x / 32f)-1) + 5; //-1, so that we have one in place in scene view
		for (int i = numSpawned; i < numToSpawn; i++) 
		{
			GameObject newFloor = (GameObject)Instantiate (floorPrefab);
			newFloor.transform.position = new Vector3(i * 32f,0, 0);
			newFloor.transform.parent = LevelObjectsParent.transform;
		}
		numSpawned = numToSpawn;	
		
		//Spawn everything else
		if (cowSpawnTimer >= spawnTime)
		{
			GameObject newCow = (GameObject)Instantiate(cowPrefab);
			newCow.transform.position = new Vector3(worldCenterObject.transform.position.x + spawnDistance,0, Random.Range(-(float)levelWidth, (float)levelWidth));
			newCow.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newCow.transform.parent = LevelObjectsParent.transform;
			
			//reset spawnTimer
			cowSpawnTimer = 0;
		}
	}
}













