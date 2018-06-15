using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {
  public GlobeMovingScript globeMovingScript;
  public RotateGlobeToMapPositionScript rotateScript;
  string geoCords = "E 68°53'00\"-E 90°52'00\"/N 34°22'00\"-N 05°00'00\"";
  
  void ClickableInterface.onClick(){
    globeMovingScript.moving = true;
    globeMovingScript.expanded = !globeMovingScript.expanded;
    rotateScript.rotateGlobe(geoCords);
  }

  public void setActiveGeoCords(string cords){
    geoCords = cords;
  }
}
