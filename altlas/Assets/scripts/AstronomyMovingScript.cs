using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomyMovingScript : MonoBehaviour {
    public float speed = 3f;
    public bool moving = false;
    public bool expanded = false;
    private Vector3 ceiling = new Vector3(0, 4.57f, 0);
    private Vector3 ceilingSize = new Vector3(6.95f, 0.006f, 6.638f);
    private float expandRate = 1.015f;
    void Update()
    {
        if (moving)
        {
            var target = expanded ? ceiling: MoveStack.MIDDLE_OF_DESK;
            if (transform.position == target)
            {
                moving = false;
                //transform.localScale = (expanded ? ceilingSize : MoveStack.MAP_ON_FREE_AREA_OF_DESK_SCALE);
                GetComponent<MapOnClick>().state = MapOnClick.MapState.ScaleGlobeToDesk;
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            transform.localScale *= (!expanded ? expandRate : (2 - expandRate));
        }
    }
}
