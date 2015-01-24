﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerColour : Photon.MonoBehaviour {

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
	void Start () {
	    if (PhotonNetwork.connected)
        {
            var color = Colors[photonView.ownerId % Colors.Length];
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
