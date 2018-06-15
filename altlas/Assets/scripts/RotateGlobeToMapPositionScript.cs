using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGlobeToMapPositionScript : MonoBehaviour {

  public GameObject mount;
  public GameObject stand;
  float radius;
  CordsParser parser = new CordsParser();
  CordsMapper mapper = new CordsMapper();
  Quaternion rotation = Quaternion.identity;
  Quaternion startRotationGlobe;
  Quaternion startRotationMount;
  Quaternion startRotationStand;

  void Start () {
   radius = gameObject.transform.localScale.x;
   startRotationGlobe = gameObject.transform.rotation;
   startRotationMount = mount.transform.rotation;
   startRotationStand = stand.transform.rotation;
  }
  
  void Update () {
    float[] points = parser.parse("W 23°03'04\"-W 22°48'57\"/N 16°54'34\"-N 16°32'50\"");
    float[] mapMiddlePoint = new float[]{(points[0] + points[1])/2 , (points[2] + points[3])/2};
    Vector3 unityMapPoint = Vector3.Normalize(mapper.GeneratePoint(mapMiddlePoint[0], mapMiddlePoint[1], radius));
    switchPoints(unityMapPoint);
    Quaternion rot = Quaternion.FromToRotation(unityMapPoint, new Vector3(0, 0, 1));
    Vector3 eulerAngles = rot.eulerAngles;
    switchPoints(eulerAngles);
    //gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.right) * startRotationGlobe;
    //mount.transform.rotation = Quaternion.AngleAxis(90, Vector3.back) * startRotationMount;
    stand.transform.rotation = Quaternion.AngleAxis(90, Vector3.up) * startRotationStand;
    //stand z
    //mount y
    //globe z
  }

  void switchPoints(Vector3 vector){
    float y = vector.y;
    float z = vector.z;
    vector.z = y; 
    vector.y = z;
  }
}
