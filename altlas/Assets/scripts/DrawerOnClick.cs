using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOnClick : MonoBehaviour, ClickableInterface
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickableInterface.onClick()
    {
        Debug.Log("drawer clicked");
    }
}
