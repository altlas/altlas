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
    public LeverScript leverScript;



    // Use this for initialization
    void Start()
    {
        leverScript = (LeverScript)Object.FindObjectOfType(typeof(LeverScript));
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
        //a map from a stack is being clicked
        if (transform.parent != null)
        {
            GameObject stack = transform.parent.gameObject;
            //a map in a stack has been clicked even though there are already maps on the desk, so first reset the old stack
            if (MoveStack.removedStack != null)
            {
                MoveStack.resetStack();
            }
            MoveStack.moveStackToDesk(stack);
        }
        else
        { //a map from the desk is being clicked
            MoveStack.moveMapToMiddleOfDesk(gameObject);
            isMoving = true;
            leverScript.setActiveMap(gameObject);
        }
    }
}
