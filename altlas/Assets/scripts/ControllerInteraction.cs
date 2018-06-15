using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerInteraction : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    public static GameObject heldObject;

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
            return;
        }
        if (controller.GetPressDown(triggerButton)) {
            triggerButtonPressedAction();
        }


    }

    public void triggerButtonPressedAction() {
        if (heldObject != null)
        {
            var clickable = heldObject.GetComponent<ClickableInterface>();
            if(clickable != null)
            {
                clickable.onClick();
            }
            if (MoveStack.objectIsFromAStack(heldObject) && MoveStack.removedStack != null)
            {
                heldObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.GetComponent<HighlightScript>() != null)
            collider.gameObject.GetComponent<HighlightScript>().OnRayEnter();
        if (collider.gameObject.GetComponent<ClickableInterface>() != null) {
            heldObject = collider.gameObject;
            var mapScript = heldObject.GetComponent<MapScript>();
            if (!MoveStack.objectIsFromAStack(heldObject) && mapScript != null)
                GameObject.Find(MoveStack.textDisplayName).GetComponent<TextMesh>().text = mapScript.data.userRelevantDataToString();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        heldObject = null;

        if (MoveStack.MAP_ON_MIDDLE_OF_DESK == null) {
            GameObject.Find(MoveStack.textDisplayName).GetComponent<TextMesh>().text = "Select a map!";
        }
        else {
            GameObject.Find(MoveStack.textDisplayName).GetComponent<TextMesh>().text = MoveStack.MAP_ON_MIDDLE_OF_DESK.GetComponent<MapScript>().data.userRelevantDataToString(); ;
        }

        if (collider.gameObject.GetComponent<HighlightScript>() != null)
            collider.gameObject.GetComponent<HighlightScript>().OnRayExit();
    }   
}