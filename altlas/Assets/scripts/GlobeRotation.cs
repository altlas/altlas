using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRotation : MonoBehaviour {
    public GameObject globe;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        globe.transform.Rotate(0, 0, 10 * Time.deltaTime);
    }
}
