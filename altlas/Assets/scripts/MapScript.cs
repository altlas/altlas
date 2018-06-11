using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {
  public MapData data;
  private Color startcolor;
  public Renderer rend;

  void start(){
  }

  public void OnMouseEnter()
  {
    rend = GetComponent<MeshRenderer>();
    startcolor = rend.material.color;
    rend.material.color = Color.red;
  }

  public void OnMouseExit()
  {
    rend = GetComponent<MeshRenderer>();
    //rend.material.color = startcolor;
  }
}
