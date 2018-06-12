using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerInteraction : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject removedStack = null;
    private Vector3 removedStackPosition;
    MapGenerator instance = new MapGenerator();

    public GameObject heldObject;
    public bool isHolding;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId leftPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    private Valve.VR.EVRButtonId upPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    private Valve.VR.EVRButtonId rightPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private Valve.VR.EVRButtonId downPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;

    private SteamVR_Controller.Device controller {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        gripButtonPressedAction();

        if (removedStack != null) {
            removedStackPosition = removedStack.transform.position;
        }


        if (controller.GetPressDown(gripButton))
        {   
            gripButtonPressedAction();
        }
        if (controller.GetPressUp(gripButton))
        {
            if (heldObject != null && isHolding)
            {
                heldObject.transform.parent = null;
                isHolding = false;
            }

        }
        if (controller.GetPressDown(triggerButton)) {
            if (removedStack != null) {
                resetStack();
            }
        }


    }

    public void gripButtonPressedAction() {
        if (heldObject != null)
        {
            if (objectIsFromAStack(heldObject))
            {
                GameObject stack = heldObject.transform.parent.gameObject;
                if (removedStack != null)
                {
                    if (stack.name.Equals(removedStack.transform.name))  //maps on desk are being clicked
                    {
                        holdObject();
                        return;
                    } else //another stack was selected while another was on the table, so first move back the maps
                    {                        
                        resetStack();
                    }
                }
                moveStackToDesk(stack);
            }
            else {
                holdObject();
            }
        }
    }

    
    public bool objectIsFromAStack(GameObject obj) {
        if (obj == null) return false;
        if (obj.transform.parent != null) {
            if (heldObject.transform.parent.name.Contains(instance.EMPTY_ENDING)) {
                return true;
            }
        }
        return false;
    }

    private void moveStackToDesk(GameObject stackToMove) {
        for (int i = 0; i < stackToMove.transform.childCount; i++)
        {
            instance.spreadGameObjectOnDesk(stackToMove.transform.GetChild(i).gameObject);
        }
        removedStack = stackToMove;
    }

    public void resetStack() {
        string nameOfMapsObjects = removedStack.transform.name.Remove(removedStack.transform.name.Length - instance.EMPTY_ENDING.Length);
        int i = 0;
        foreach (GameObject map in GameObject.FindGameObjectsWithTag("Pickupable"))
            {
                if (map.name.Equals(nameOfMapsObjects))
                {
                    map.transform.position = new Vector3(removedStackPosition.x, removedStackPosition.y + i* instance.MAPS_Y_OFFSET, removedStackPosition.z);
                    map.transform.localScale = new Vector3(0.1f, map.transform.localScale.y, 0.1f);
                    map.transform.localRotation = Quaternion.identity;
                    if (map.transform.parent == null) {
                    map.transform.parent = removedStack.transform;
                    }
                }
             i++;
         }
        removedStack = null;
    }

    private void holdObject() {
        heldObject.transform.parent = this.transform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        isHolding = true;
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Pickupable") && !isHolding) {
            heldObject = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(!isHolding)
            heldObject = null;
    }
}