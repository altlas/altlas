using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {
  public MapData data;
  private Color startcolor;
  public Renderer rend;

  void start(){
  }

  public void OnRayEnter()
  {
    rend = GetComponent<MeshRenderer>();
    rend.material.SetColor("_EmissionColor", Color.red);
  }

  public void OnRayExit()
  {
    print("remove me oh noez");
    rend = GetComponent<MeshRenderer>();
    rend.material.SetColor("_EmissionColor", Color.black);
  }
}
