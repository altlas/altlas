using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {
  public GlobeMovingScript globeMovingScript;

	void Start () {
	}
	
	void Update () {
		
	}

  void ClickableInterface.onClick(){
    globeMovingScript.moving = true;
    globeMovingScript.expanded = !globeMovingScript.expanded;
  }
}
