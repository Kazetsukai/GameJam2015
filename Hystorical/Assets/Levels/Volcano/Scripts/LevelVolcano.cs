using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelVolcano : MonoBehaviour {

	public GameObject worldCenterObject;
	public GameObject objectDespawner;

	public GameObject boulderPrefab;
	public GameObject treePrefab;
	public GameObject plantPrefab;
	public GameObject rockPrefab;

	public float levelWidth = 17f;

	public int maxBoulders = 3;
	public int maxTrees = 3;
	public int maxPlants = 3;

	public List<GameObject> boulderList = new List<GameObject>();
	public List<GameObject> treeList = new List<GameObject>();
	public List<GameObject> plantList = new List<GameObject>();


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (boulderList.Count < maxBoulders)
		{
			GameObject newBoulder = (GameObject)Instantiate(boulderPrefab, worldCenterObject.transform.position + new Vector3(10,0, Random.Range(-levelWidth / 2, levelWidth / 2)), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
			newBoulder.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1f, 4f);
			boulderList.Add(newBoulder);
		}



	}
}
