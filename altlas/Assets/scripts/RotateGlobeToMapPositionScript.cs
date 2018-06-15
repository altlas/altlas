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

    float progress = 0.0f;
    float step = -0.5f;


  void Start () {
   radius = -gameObject.transform.localScale.x;
   startRotationGlobe = gameObject.transform.localRotation;
   startRotationMount = mount.transform.localRotation;
   startRotationStand = stand.transform.localRotation;
  }
  
  void Update () {
        step = (progress > 0 && progress < 1) ? step : -step;
        progress += Time.deltaTime * step;
        float angle = progress * 90;
    float[] points = parser.parse("E 68°53'00\"-E 90°52'00\"/N 34°22'00\"-N 05°00'00\"");
    float[] mapMiddlePoint = new float[]{(points[0] + points[1])/2 , (points[2] + points[3])/2};
        Vector3 unityMapPoint = Vector3.Normalize(mapper.GeneratePoint(mapMiddlePoint[1], mapMiddlePoint[0], radius));
    switchPoints(unityMapPoint);
    Quaternion rot = Quaternion.FromToRotation(unityMapPoint, new Vector3(0, 0, 1));
    Vector3 eulerAngles = rot.eulerAngles;
    switchPoints(eulerAngles);
    gameObject.transform.localRotation = Quaternion.AngleAxis(eulerAngles.x - 90, Vector3.forward) * startRotationGlobe;
    mount.transform.localRotation = Quaternion.AngleAxis(-eulerAngles.y, Vector3.up) * startRotationMount;
    stand.transform.localRotation = Quaternion.AngleAxis(eulerAngles.z, Vector3.forward) * startRotationStand;
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
