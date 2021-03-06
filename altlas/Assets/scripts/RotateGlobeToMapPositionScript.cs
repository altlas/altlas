﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGlobeToMapPositionScript : MonoBehaviour {

  public GameObject mount;
  public GameObject stand;
  float radius;
  CordsParser parser = new CordsParser();
  CordsMapper mapper = new CordsMapper();
  Quaternion startRotationGlobe;
  Quaternion startRotationMount;
  Quaternion startRotationStand;


  void Start () {
   radius = -gameObject.transform.localScale.x;
   startRotationGlobe = gameObject.transform.localRotation;
   startRotationMount = mount.transform.localRotation;
   startRotationStand = stand.transform.localRotation;
  }
  
  void Update () {
    
  }

  public void rotateGlobe(string worldCords){
    float[] points = parser.parse(worldCords);
    float[] mapMiddlePoint = new float[]{(points[0] + points[1])/2 , (points[2] + points[3])/2};
    Vector3 unityMapPoint = Vector3.Normalize(mapper.GeneratePoint(mapMiddlePoint[0], mapMiddlePoint[1], radius));
    switchPoints(unityMapPoint);
    Quaternion rot = Quaternion.FromToRotation(unityMapPoint, new Vector3(0, 0, 1));
    Vector3 eulerAngles = rot.eulerAngles;
    switchPoints(eulerAngles);
    gameObject.transform.localRotation = Quaternion.AngleAxis(eulerAngles.x - 90, Vector3.forward) * startRotationGlobe;
    mount.transform.localRotation = Quaternion.AngleAxis(-eulerAngles.y, Vector3.up) * startRotationMount;
    stand.transform.localRotation = Quaternion.AngleAxis(eulerAngles.z, Vector3.forward) * startRotationStand;
  }

  void switchPoints(Vector3 vector){
    float y = vector.y;
    float z = vector.z;
    vector.z = y; 
    vector.y = z;
  }
}
