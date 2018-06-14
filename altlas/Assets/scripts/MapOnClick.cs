using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOnClick : MonoBehaviour, ClickableInterface
{
    public float speed = 1f;
    public Vector3 targetLocation = new Vector3(-0.2f, 0.858f, 0.65f);
    private bool isMoving = false;
    private Vector3 startPosition;
    public bool isNormlScale = true;
    public bool isLargeScale = false;
    public bool isGettingLarger = false;



    // Use this for initialization
    void Start()
    {

    }

    void Update() {
        if (isMoving)
        {
            var target = MoveStack.MAP_ON_MIDDLE_OF_DESK!=null ? targetLocation : startPosition;
            if (transform.position == target)
            {
                isMoving = false;
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    void ClickableInterface.onClick()
    {
        if (this.transform.parent == null) {
            isMoving = true;
        }
           
        if (MoveStack.removedStack != null) {
            if (MoveStack.MAP_ON_MIDDLE_OF_DESK != null) {
                MoveStack.MAP_ON_MIDDLE_OF_DESK.transform.position = this.transform.position;
                MoveStack.MAP_ON_MIDDLE_OF_DESK.transform.localScale = MoveStack.MAP_ON_FREE_AREA_OF_DESK_SCALE;
            }
            MoveStack.MAP_ON_MIDDLE_OF_DESK = this.gameObject;
            GameObject.Find(MoveStack.textDisplayName).GetComponent<TextMesh>().text = MoveStack.MAP_ON_MIDDLE_OF_DESK.GetComponent<MapScript>().data.userRelevantDataToString();
        }
    }
}
