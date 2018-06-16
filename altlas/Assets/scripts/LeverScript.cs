using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, ClickableInterface {
  public GlobeMovingScript globeMovingScript;
  public RotateGlobeToMapPositionScript rotateScript;
  public LaserEffect laserEffect;
  float timeLeft;
    bool trigger = false;
  GameObject map;
    bool moving = false;
    bool active = false;
    int step = 3;
  
  
    void Update()
    {
        if (moving)
        {
            var alpha = active ? 60 : -60; 
            gameObject.transform.GetChild(0).Rotate(new Vector3(1, 0, 0), alpha);
            step--;
            if (step == 0)
            {
                active = !active;
                moving = false;
                step = 3;
            }
        }
        timeLeft -= Time.deltaTime;
        if(trigger && timeLeft < 0)
        {
            //moveGlobe();
            trigger = false;
        }
    }
  void ClickableInterface.onClick(){
    if (globeMovingScript.expanded) {
            //map.GetComponent<MapOnClick>().state = MapOnClick.MapState.ScaleGlobeToDesk;
            globeMovingScript.moving = true;
        }
    else {
        map.GetComponent<MapOnClick>().state = MapOnClick.MapState.ScaleDeskToGlobe;
            laserEffect.shootLasers(map);
            rotateScript.rotateGlobe(map.GetComponent<MapScript>().data.m_coordinate);
    }
    globeMovingScript.expanded = !globeMovingScript.expanded;
    if (map != null){
      timeLeft = 2f;
      moving = true;
      trigger = true;
      //rotateScript.rotateGlobe(map.GetComponent<MapScript>().data.m_coordinate);
    }
  }
  
  void moveGlobe()
    {
        globeMovingScript.moving = true;
        globeMovingScript.expanded = !globeMovingScript.expanded;
    }
  public void setActiveMap(GameObject m){
    map = m;
  }
}
