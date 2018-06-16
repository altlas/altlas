using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour {
    Vector3 robotLeftStartPoint;
    Vector3 robotRightStartPoint;
    public LineRenderer laser;
    public Material laserMaterial;
    float timeLeft;

    void Start () {
        robotLeftStartPoint = gameObject.transform.GetChild(0).GetChild(11).position;
        robotRightStartPoint = gameObject.transform.GetChild(1).GetChild(11).position;
        
        laser.positionCount = 3;
        laser.GetComponent<Renderer>().material = laserMaterial;
        laser.startWidth = 0.005f;
        laser.endWidth = 0.005f;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft< 0)
        {
            laser.enabled = false;
        }
    }

    public void shootLasers(GameObject map)
    {
        timeLeft = 2;
        laser.enabled = true;
        laser.SetPosition(0, robotLeftStartPoint);
        laser.SetPosition(1, map.transform.position);
        laser.SetPosition(2, robotRightStartPoint);
    }
}
