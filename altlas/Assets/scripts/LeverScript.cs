using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {
  public GlobeMovingScript globeMovingScript;
  public RotateGlobeToMapPositionScript rotateScript;
    GameObject map;// = "E 68°53'00\"-E 90°52'00\"/N 34°22'00\"-N 05°00'00\"";
  
  void ClickableInterface.onClick(){
    if (!globeMovingScript.expanded)
    {
        map.GetComponent<MapOnClick>().state = MapOnClick.MapState.ScaleDeskToGlobe;
    }
    globeMovingScript.moving = true;
    globeMovingScript.expanded = !globeMovingScript.expanded;
    rotateScript.rotateGlobe(map.GetComponent<MapScript>().data.m_coordinate);
  }

  public void setActiveMap(GameObject m){
    map = m;
  }
}
