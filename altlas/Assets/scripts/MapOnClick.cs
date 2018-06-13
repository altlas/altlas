using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOnClick : MonoBehaviour, ClickableInterface
{
    Vector3 MIDDLE_OF_DESK = new Vector3(-0.2f, 0.858f, 0.65f); //absolute positon
    public static GameObject MAP_ON_MIDDLE_OF_DESK = null; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClickableInterface.onClick()
    {
        if (MoveStack.removedStack != null) {
            if (MAP_ON_MIDDLE_OF_DESK != null) {
                MAP_ON_MIDDLE_OF_DESK.transform.position = this.transform.position;
                MAP_ON_MIDDLE_OF_DESK.transform.localScale = MoveStack.MAP_ON_FREE_AREA_OF_DESK_SCALE;
            }
            this.transform.position = MIDDLE_OF_DESK;
            this.transform.localRotation = Quaternion.identity;
            this.transform.localScale = MoveStack.MAP_ON_MIDDLE_OF_DESK_SCALE;
            MAP_ON_MIDDLE_OF_DESK = this.gameObject;
        }
    }
}
