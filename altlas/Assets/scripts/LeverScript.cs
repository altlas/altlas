using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {
  GameObject globe;

	void Start () {
		globe = GameObject.Find("globe");
	}
	
	void Update () {
		
	}

  void ClickableInterface.onClick(){
    globe.GetComponent<GlobeMovingScript>().moving = true;
    globe.GetComponent<GlobeMovingScript>().expanded = !globe.GetComponent<GlobeMovingScript>().expanded;
  }
}
