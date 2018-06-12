using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour {
  public Renderer rend;

  public void OnRayEnter()
  {
    rend = GetComponent<MeshRenderer>();
    rend.material.SetColor("_EmissionColor", new Color(0.3f, 0.3f, 0.3f));
  }

  public void OnRayExit()
  {
    rend = GetComponent<MeshRenderer>();
    rend.material.SetColor("_EmissionColor", Color.black);
  }
}
