using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeMovingScript : MonoBehaviour {
  public float speed = 1f;
  public bool expanded = false;
  public bool moving = false;
  private Vector3 outOffset = new Vector3(0, 0.9f, 0);
  private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
	}
	
	void Update () {
    if (moving)
      {
        var target = expanded ? startPosition + outOffset : startPosition;
        if(transform.position == target)
        {
            moving = false;
            return;
        }
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
      }
	}
}
