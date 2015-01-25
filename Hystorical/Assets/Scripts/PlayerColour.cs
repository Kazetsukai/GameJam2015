using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerColour : Photon.MonoBehaviour {

    public bool NPC = false;

    static Color[] Colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.magenta,
        Color.white,
        Color.black,
        Color.cyan
    };

    static string[] MaterialsToChange = new string[]
    {
        "person_1-armLUpper",
        "person_1-armRUpper",
        "person_1-legLThigh",
        "person_1-legRThigh",
        "person_1-hips",
        "person_1-torsoUpper",
        "person_1-torsoLower",
    };

	// Use this for initialization
	void Start () 
    {
        Color color;
	    if (!NPC && PhotonNetwork.connected)
        {
            color = Colors[photonView.ownerId % Colors.Length];
        }
        else
        {
            color = new Color(Random.Range(0.5f, 0.8f),Random.Range(0.5f, 0.8f),Random.Range(0.5f, 0.8f));
        }

        var renderers = GetComponentsInChildren<Renderer>(true);

        foreach (var renderer in renderers)
        {
            foreach (var material in renderer.materials)
            {
                if (MaterialsToChange.Any(m => material.name.StartsWith(m)))
                {
                    material.color = color;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
