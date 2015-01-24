using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelVolcano : Photon.MonoBehaviour {

	public GameObject camera;
	public GameObject worldCenterObject;
	private GameObject LevelObjectsParent;

	public GameObject floorPrefab;
	public GameObject boulderPrefab;
	public GameObject treePrefab;
	public GameObject plantPrefab;
	public GameObject rockPrefab;

	public float levelWidth = 30f;

	public float boulderSpawnRate = 1f;
	public float treeSpawnRate = 1f;
	public float plantSpawnRate = 1f;
	public float rockSpawnRate = 1f;

	public float spawnTime = 5f;

	public float boulderSpawnTimer = 0f;
	public float treeSpawnTimer = 0f;
	public float plantSpawnTimer = 0f;
	public float rockSpawnTimer = 0f;

	public float spawnDistance = 60f; //distance objects spawn from worldCenterObject

	public int numSpawned = 0;

	float objectDespawnerDistance = 120f;

	public GameObject lava;
	float lavaDistance = 21f;

	public GameObject LevelBounds;
	public GameObject ObjectDespawner;

	public float levelSlideSpeed = -8f;

	// Use this for initialization
	void Start () 
	{
		camera.GetComponent<MoveAllTheThings> ().Slide = new Vector3(levelSlideSpeed,0,0);
	
		//spawn lava thing
		LevelObjectsParent = new GameObject ("LevelObjects");
		LevelObjectsParent.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{	
		//Update positions
		ObjectDespawner.transform.position = worldCenterObject.transform.position - new Vector3(objectDespawnerDistance, 0 , 0);
		lava.transform.position = new Vector3(worldCenterObject.transform.position.x - lavaDistance, lava.transform.position.y , lava.transform.position.z);
		LevelBounds.transform.position = new Vector3(worldCenterObject.transform.position.x, LevelBounds.transform.position.y, LevelBounds.transform.position.z);
		
		//Spawn floor objects as worldCenterObject moves
		int numToSpawn = ((int)(worldCenterObject.transform.position.x / 32f)-1) + 5; //-1, so that we have one in place in scene view
		for (int i = numSpawned; i < numToSpawn; i++) 
		{
			GameObject newFloor = (GameObject)Instantiate (floorPrefab);
			newFloor.transform.position = new Vector3(i * 32f,0, 0);
			newFloor.transform.parent = LevelObjectsParent.transform;
		}
		numSpawned = numToSpawn;

		if (PhotonNetwork.isMasterClient)
		{
			//update timers
			boulderSpawnTimer += Time.fixedDeltaTime * boulderSpawnRate;
			treeSpawnTimer += Time.fixedDeltaTime * treeSpawnRate;
			plantSpawnTimer += Time.fixedDeltaTime * plantSpawnRate;
			rockSpawnTimer += Time.fixedDeltaTime * rockSpawnRate;
	

			//Spawn everything else
			if (boulderSpawnTimer >= spawnTime)
			{
				var pos = new Vector3(worldCenterObject.transform.position.x + spawnDistance,0, Random.Range(-(float)levelWidth, (float)levelWidth));
				var rotation = new Vector3(0, Random.Range(0, Mathf.PI * 2), 0);
				var scale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
				
				photonView.RPC ("SpawnBoulder", PhotonTargets.All, pos, rotation, scale);

				//reset spawnTimer
				boulderSpawnTimer = 0;
			}

			if (plantSpawnTimer >= spawnTime)
			{
				var pos = new Vector3(worldCenterObject.transform.position.x + spawnDistance,0, Random.Range(-(float)levelWidth, (float)levelWidth));
				var rotation = new Vector3(0, Random.Range(0, Mathf.PI * 2), 0);
				var scale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
				
				photonView.RPC ("SpawnPlant", PhotonTargets.All, pos, rotation, scale);

				//reset spawnTimer
				plantSpawnTimer = 0;
			}
			if (treeSpawnTimer >= spawnTime)
			{
				var pos = new Vector3(worldCenterObject.transform.position.x + spawnDistance,0, Random.Range(-(float)levelWidth, (float)levelWidth));
				var rotation = new Vector3(0, Random.Range(0, Mathf.PI * 2), 0);
				var scale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);

				photonView.RPC ("SpawnTree", PhotonTargets.All, pos, rotation, scale);

				//reset spawnTimer
				treeSpawnTimer = 0;
			}

			if (rockSpawnTimer >= spawnTime)
			{
				var pos = worldCenterObject.transform.position + new Vector3(Random.Range (0, 50f),30, Random.Range(-levelWidth / 2, levelWidth / 2));
				var rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
				var scale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);

				photonView.RPC ("SpawnRock", PhotonTargets.All, pos, rotation, scale);

				//reset spawnTimer
				rockSpawnTimer = 0;
			}
		}
	}

	[RPC]
	public void SpawnBoulder(Vector3 pos, Vector3 rotation, Vector3 scale)
	{
		GameObject newBoulder = (GameObject)Instantiate(boulderPrefab);
		newBoulder.transform.position = pos;
		newBoulder.transform.localEulerAngles += rotation;
		newBoulder.transform.localScale = scale;
		newBoulder.transform.parent = LevelObjectsParent.transform;
	}

	[RPC]
	public void SpawnPlant(Vector3 pos, Vector3 rotation, Vector3 scale)
	{
		GameObject newPlant = (GameObject)Instantiate(plantPrefab);
		newPlant.transform.position = pos;
		newPlant.transform.localEulerAngles += rotation;
		newPlant.transform.localScale = scale;
		newPlant.transform.parent = LevelObjectsParent.transform;
	}

	[RPC]
	public void SpawnTree(Vector3 pos, Vector3 rotation, Vector3 scale)
	{
		GameObject newTree = (GameObject)Instantiate(treePrefab);
		newTree.transform.position = pos;
		newTree.transform.localEulerAngles += rotation;
		newTree.transform.localScale = scale;
		newTree.transform.parent = LevelObjectsParent.transform;
	}

	[RPC]
	public void SpawnRock(Vector3 pos, Quaternion rotation, Vector3 scale)
	{
		GameObject newRock = (GameObject)Instantiate(rockPrefab, pos, rotation);
		newRock.transform.localScale = scale;
		newRock.transform.parent = LevelObjectsParent.transform;
	}
}













