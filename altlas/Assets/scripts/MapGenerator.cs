using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
	private Loader loader;

	// Use this for initialization
	void Start () {
        loader = GetComponent<Loader>();
        if (loader.data.Count != 0)
        {
            Debug.Log("XML data of first: " + loader.data[0].m_year);
        }

        for (int i = 0; i < 10; i++) {
            Instantiate(map, new Vector3(-0.8938485f, 1.267f, 0.7878597f), Quaternion.identity);
  
        }

    }


    // Update is called once per frame
    void Update () {
        if (loader.data.Count != 0)
        {
            Debug.Log("XML data of first: " + loader.data[0].m_year);
        }
    }
}
