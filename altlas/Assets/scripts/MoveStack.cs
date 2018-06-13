using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStack {

    private static string inputDeviceName = "Controller"; //see resetStack() if you want another form of Input
    public static GameObject removedStack = null;
    public static Vector3 removedStackPosition;
    private static MapGenerator instance = new MapGenerator();
    


    public static bool objectIsFromAStack(GameObject obj)
    {
        if (obj == null) return false;
        if (obj.transform.parent != null)
        {
            if (obj.transform.parent.name.Contains(instance.EMPTY_ENDING))
            {
                return true;
            }
        }
        return false;
    }

    public static void moveStackToDesk(GameObject stackToMove)
    {
        for (int i = 0; i < stackToMove.transform.childCount; i++)
        {
            instance.spreadGameObjectOnDesk(stackToMove.transform.GetChild(i).gameObject);
        }
        removedStack = stackToMove;
    }

    public static void resetStack()
    {
        string nameOfMapsObjects = removedStack.transform.name.Remove(removedStack.transform.name.Length - instance.EMPTY_ENDING.Length);
        int i = 0;
        foreach (GameObject map in GameObject.FindGameObjectsWithTag("Pickupable"))
        {
            if (map.name.Equals(nameOfMapsObjects))
            {
                if (map.transform.parent == null || map.transform.parent.name.Contains("Controller"))
                {
                    map.transform.parent = removedStack.transform;
                }
                map.transform.position = new Vector3(removedStackPosition.x, removedStackPosition.y + i * instance.MAPS_Y_OFFSET, removedStackPosition.z);
                map.transform.localScale = new Vector3(0.1f, map.transform.localScale.y, 0.1f);
                map.transform.localRotation = Quaternion.identity;
            }
            i++;
        }
        removedStack = null;
    }
}
