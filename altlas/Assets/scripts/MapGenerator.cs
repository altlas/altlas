using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
	// Use this for initialization
	void Start () {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Instantiate(map, new Vector3(-0.085f, 1, 0), Quaternion.identity);
            }
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
