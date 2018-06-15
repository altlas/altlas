using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {

  public GlobeMovingScript globeMovingScript;

    void ClickableInterface.onClick(){
        GameObject mapInMiddle = MoveStack.MAP_ON_MIDDLE_OF_DESK;
        if (mapInMiddle != null) {
            if (mapInMiddle.name.Equals("astronomie"))
            {
                AstronomyMovingScript astronomieScript = mapInMiddle.GetComponent<AstronomyMovingScript>();
                astronomieScript.moving = true;
                astronomieScript.expanded = !astronomieScript.expanded;
                return;
            }
        }
        globeMovingScript.moving = true;
        globeMovingScript.expanded = !globeMovingScript.expanded;
    
  }
}
