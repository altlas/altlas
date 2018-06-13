using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerInteraction : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    /*private GameObject removedStack = null;
    private Vector3 removedStackPosition;
    MapGenerator instance = new MapGenerator();*/

    public GameObject heldObject;
    public bool isHolding;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId leftPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    private Valve.VR.EVRButtonId upPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    private Valve.VR.EVRButtonId rightPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private Valve.VR.EVRButtonId downPadButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;

     SteamVR_Controller.Device controller {
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
        if (MoveStack.removedStack != null) {
            MoveStack.removedStackPosition = MoveStack.removedStack.transform.position;
        }


        if (controller.GetPressDown(gripButton))
        {
            if (heldObject != null)
            {
                if (MoveStack.objectIsFromAStack(heldObject)) {
                    var clickable = heldObject.GetComponent<ClickableInterface>();
                    if (clickable != null)
                    {
                        clickable.onClick();
                        return;
                    }
                }
            }
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
            triggerButtonPressedAction();
        }


    }

    public void triggerButtonPressedAction() {
        if (heldObject != null)
        {
            if (MoveStack.objectIsFromAStack(heldObject))
            {
                GameObject stack = heldObject.transform.parent.gameObject;
                if (MoveStack.removedStack != null)
                {
                    if (stack.name.Equals(MoveStack.removedStack.transform.name))  //maps on desk are being clicked
                    {
                        return;
                    }
                    //another stack was selected while another was on the table, so first move back the maps        
                    MoveStack.resetStack();
                    isHolding = false;
                    heldObject = null;
                }
                MoveStack.moveStackToDesk(stack);
            }
        }
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
        if (collider.gameObject.GetComponent<HighlightScript>() != null)
            collider.gameObject.GetComponent<HighlightScript>().OnRayEnter();
    }

    private void OnTriggerExit(Collider collider)
    {
        if(!isHolding)
            heldObject = null;

        if (collider.gameObject.GetComponent<HighlightScript>() != null)
            collider.gameObject.GetComponent<HighlightScript>().OnRayExit();
    }   
}