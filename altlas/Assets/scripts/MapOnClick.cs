using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOnClick : MonoBehaviour, ClickableInterface
{
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
            if (MoveStack.MAP_ON_MIDDLE_OF_DESK != null) {
                MoveStack.MAP_ON_MIDDLE_OF_DESK.transform.position = this.transform.position;
                MoveStack.MAP_ON_MIDDLE_OF_DESK.transform.localScale = MoveStack.MAP_ON_FREE_AREA_OF_DESK_SCALE;
            }
            this.transform.position = MoveStack.MIDDLE_OF_DESK;
            this.transform.localRotation = Quaternion.identity;
            this.transform.localScale = MoveStack.MAP_ON_MIDDLE_OF_DESK_SCALE;
            MoveStack.MAP_ON_MIDDLE_OF_DESK = this.gameObject;
            GameObject.Find(MoveStack.textDisplayName).GetComponent<TextMesh>().text = MoveStack.MAP_ON_MIDDLE_OF_DESK.GetComponent<MapScript>().data.userRelevantDataToString();
        }
    }
}
