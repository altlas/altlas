using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 10; i++) {
        Instantiate(map, new Vector3(-0.8938485f, 1.267f, 0.7878597f), Quaternion.identity);
        }

    }


    // Update is called once per frame
    void Update () {
		
	}
}
