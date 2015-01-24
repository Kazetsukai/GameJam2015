using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelVolcano : MonoBehaviour {

	public GameObject worldCenterObject;
	private GameObject LevelObjectsParent;
	public GameObject objectDespawnerPrefab;

	public GameObject lavaPrefab;
	public GameObject floorPrefab;
	public GameObject boulderPrefab;
	public GameObject treePrefab;
	public GameObject plantPrefab;
	public GameObject rockPrefab;

	public float levelWidth = 32f;

	public float boulderSpawnRate = 1f;
	public float treeSpawnRate = 1f;
	public float plantSpawnRate = 1f;
	public float rockSpawnRate = 1f;

	public float spawnTime = 5f;

	public float boulderSpawnTimer = 0f;
	public float treeSpawnTimer = 0f;
	public float plantSpawnTimer = 0f;
	public float rockSpawnTimer = 0f;

	public float spawnDistance = 40f; //distance objects spawn from worldCenterObject

	public int numSpawned = 0;

	GameObject lava;
	Vector3 lavaOffset;

	GameObject objectDespawner;
	float objectDespawnerDistance = 80f;

	// Use this for initialization
	void Start () 
	{
		//spawn lava thing
		LevelObjectsParent = new GameObject ("LevelObjects");
		LevelObjectsParent.transform.parent = this.transform;

		objectDespawner = (GameObject)Instantiate (objectDespawnerPrefab);


		lava = (GameObject)Instantiate (lavaPrefab);
		lava.transform.position = new Vector3 (-21.38f, 0.34f, -4.11f);
		lavaOffset = worldCenterObject.transform.position - lava.transform.position;
		lava.transform.parent = LevelObjectsParent.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Update positions
		objectDespawner.transform.position = worldCenterObject.transform.position - new Vector3(objectDespawnerDistance, 0 , 0);
		lava.transform.position = worldCenterObject.transform.position - lavaOffset;

		//update timers
		boulderSpawnTimer += Time.fixedDeltaTime * boulderSpawnRate;
		treeSpawnTimer += Time.fixedDeltaTime * treeSpawnRate;
		plantSpawnTimer += Time.fixedDeltaTime * plantSpawnRate;
		rockSpawnTimer += Time.fixedDeltaTime * rockSpawnRate;

		//Spawn floor objects as worldCenterObject moves
		int numToSpawn = (int)(worldCenterObject.transform.position.x / 32f) + 5;
		for (int i = numSpawned; i < numToSpawn; i++) 
		{
			GameObject newFloor = (GameObject)Instantiate (floorPrefab);
			newFloor.transform.position = new Vector3(i * 32f,0, 0);
			newFloor.transform.parent = LevelObjectsParent.transform;
		}
		numSpawned = numToSpawn;	

		//Spawn everything else
		if (boulderSpawnTimer >= spawnTime)
		{
			GameObject newBoulder = (GameObject)Instantiate(boulderPrefab);
			newBoulder.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newBoulder.transform.localEulerAngles = new Vector3(newBoulder.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newBoulder.transform.localEulerAngles.z);
			newBoulder.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newBoulder.transform.parent = LevelObjectsParent.transform;

			//reset spawnTimer
			boulderSpawnTimer = 0;
		}

		if (plantSpawnTimer >= spawnTime)
		{
			GameObject newPlant = (GameObject)Instantiate(plantPrefab);
			newPlant.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newPlant.transform.localEulerAngles = new Vector3(newPlant.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newPlant.transform.localEulerAngles.z);
            newPlant.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newPlant.transform.parent = LevelObjectsParent.transform;

			//reset spawnTimer
			plantSpawnTimer = 0;
		}
		if (treeSpawnTimer >= spawnTime)
		{
			GameObject newTree = (GameObject)Instantiate(treePrefab);
			newTree.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newTree.transform.localEulerAngles = new Vector3(newTree.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newTree.transform.localEulerAngles.z);
			newTree.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newTree.transform.parent = LevelObjectsParent.transform;

			//reset spawnTimer
			treeSpawnTimer = 0;
		}

		if (rockSpawnTimer >= spawnTime)
		{
			GameObject newRock = (GameObject)Instantiate(rockPrefab, worldCenterObject.transform.position + new Vector3(Random.Range (-20f, 20f),30, Random.Range(-levelWidth / 2, levelWidth / 2)), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
			newRock.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newRock.transform.parent = LevelObjectsParent.transform;

			//reset spawnTimer
			rockSpawnTimer = 0;
		}
	}
}













