using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopCam : MonoBehaviour {
  public float speedH = 2.0f;
  public float speedV = 2.0f;

  private float yaw = 0.0f;
  private float pitch = 0.0f;
  private Camera cam;

  void Start () {
    Cursor.lockState = CursorLockMode.Locked;
    cam = GetComponent<Camera>();
  }
  
  void Update () {
    yaw += speedH * Input.GetAxis("Mouse X");
    pitch -= speedV * Input.GetAxis("Mouse Y");

    transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit)){
      if(hit.transform.name.ToString() == "Map_alt(Clone)"){
        var gameObject = hit.transform.gameObject;
        gameObject.GetComponent<MapScript>().OnMouseEnter();
      }

    }
      //print("I'm looking at nothing!");
  
  }
}
