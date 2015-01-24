using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelVolcano : MonoBehaviour {

	public GameObject worldCenterObject;
	public GameObject LevelObjectsParentEmpty;
	public GameObject objectDespawner;

	public GameObject floorPrefab;
	public GameObject boulderPrefab;
	public GameObject treePrefab;
	public GameObject plantPrefab;
	public GameObject rockPrefab;

	public float levelWidth = 17f;

	private float floorSpawnRate = 1f;
	public float boulderSpawnRate = 1f;
	public float treeSpawnRate = 1f;
	public float plantSpawnRate = 1f;
	public float rockSpawnRate = 1f;

	public float spawnTime = 5f;

	private float floorSpawnTimer = 0f;
	public float boulderSpawnTimer = 0f;
	public float treeSpawnTimer = 0f;
	public float plantSpawnTimer = 0f;
	public float rockSpawnTimer = 0f;

	public float spawnDistance = 40f; //distance objects spawn from worldCenterObject

	private int floorCount = 0;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//update timers
		floorSpawnTimer += Time.fixedDeltaTime * floorSpawnRate;
		boulderSpawnTimer += Time.fixedDeltaTime * boulderSpawnRate;
		treeSpawnTimer += Time.fixedDeltaTime * treeSpawnRate;
		plantSpawnTimer += Time.fixedDeltaTime * plantSpawnRate;
		rockSpawnTimer += Time.fixedDeltaTime * rockSpawnRate;

		//Spawn floor objects as worldCenterObject moves
		if (floorSpawnTimer >= spawnTime)
		{ 
			GameObject newFloor = (GameObject)Instantiate(floorPrefab);
			newFloor.transform.position = new Vector3(floorCount * 32,0, 0);
			floorCount++;
			//newFloor.transform.parent = LevelObjectsParentEmpty.transform;// DON'T DO THIS!! Makes floor slide along map...	

			//reset spawnTimer
			floorSpawnTimer = 0;
		}

		//Spawn everything else
		if (boulderSpawnTimer >= spawnTime)
		{
			GameObject newBoulder = (GameObject)Instantiate(boulderPrefab);
			newBoulder.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newBoulder.transform.localEulerAngles = new Vector3(newBoulder.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newBoulder.transform.localEulerAngles.z);
			newBoulder.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newBoulder.transform.parent = LevelObjectsParentEmpty.transform;

			//reset spawnTimer
			boulderSpawnTimer = 0;
		}

		if (plantSpawnTimer >= spawnTime)
		{
			GameObject newPlant = (GameObject)Instantiate(plantPrefab);
			newPlant.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newPlant.transform.localEulerAngles = new Vector3(newPlant.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newPlant.transform.localEulerAngles.z);
            newPlant.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newPlant.transform.parent = LevelObjectsParentEmpty.transform;

			//reset spawnTimer
			plantSpawnTimer = 0;
		}
		if (treeSpawnTimer >= spawnTime)
		{
			GameObject newTree = (GameObject)Instantiate(treePrefab);
			newTree.transform.position = worldCenterObject.transform.position + new Vector3(spawnDistance,0, Random.Range(-levelWidth / 2, levelWidth / 2));
			newTree.transform.localEulerAngles = new Vector3(newTree.transform.localEulerAngles.x, Random.Range(0, Mathf.PI * 2), newTree.transform.localEulerAngles.z);
			newTree.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newTree.transform.parent = LevelObjectsParentEmpty.transform;

			//reset spawnTimer
			treeSpawnTimer = 0;
		}

		if (rockSpawnTimer >= spawnTime)
		{
			GameObject newRock = (GameObject)Instantiate(rockPrefab, worldCenterObject.transform.position + new Vector3(Random.Range (-20f, 20f),30, Random.Range(-levelWidth / 2, levelWidth / 2)), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
			newRock.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			newRock.transform.parent = LevelObjectsParentEmpty.transform;

			//reset spawnTimer
			rockSpawnTimer = 0;
		}
	}
}













