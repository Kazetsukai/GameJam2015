using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    public float LevelChangeTime;

	// Use this for initialization
	void Start () {
	
	}

    float timePassed = 0;

	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;

        if (timePassed > LevelChangeTime)
        {
            timePassed = 0;
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
	}
}
