using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveStack {

    private static string inputDeviceName = "Controller"; //see resetStack() if you want another form of Input
    public static GameObject removedStack = null;
    public static Vector3 removedStackPosition;
    public static string MAPTAG = "Pickupable";

    public static Vector3 DESK_FREE_AREA_LEFT_CORNER = new Vector3(-0.7105434f, 0.85f, 0.4124395f);
    public static float DESK_FREE_AREA_LENGTH = 0.4f;
    public static Vector3 MAP_IN_DRAWER_SCALE = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Map_alt.prefab", typeof(GameObject))).transform.localScale;
    public static Vector3 MAP_ON_FREE_AREA_OF_DESK_SCALE = MAP_IN_DRAWER_SCALE * 2;
    public static Vector3 MAP_ON_MIDDLE_OF_DESK_SCALE = MAP_ON_FREE_AREA_OF_DESK_SCALE * 3;




    public static bool objectIsFromAStack(GameObject obj)
    {
        if (obj == null) return false;
        if (obj.transform.parent != null)
        {
            if (obj.transform.parent.name.Contains(MapGenerator.EMPTY_ENDING))
            {
                return true;
            }
        }
        return false;
    }

    public static void moveStackToDesk(GameObject stackToMove)
    {
        int numberOfMapsInStack = stackToMove.transform.childCount;
        for (int i = 0; i < numberOfMapsInStack; i++)
        {
            spreadMapOnDesk(stackToMove.transform.GetChild(0).gameObject);
            stackToMove.transform.GetChild(0).gameObject.transform.parent = null;
        }
        removedStack = stackToMove;
    }

    public static void resetStack()
    {
        string nameOfMapsObjects = removedStack.transform.name.Remove(removedStack.transform.name.Length - MapGenerator.EMPTY_ENDING.Length);
        int i = 0;
        foreach (GameObject map in GameObject.FindGameObjectsWithTag(MAPTAG))
        {
            if (map.name.Equals(nameOfMapsObjects))
            {
                if (map.transform.parent == null || map.transform.parent.name.Contains(inputDeviceName))
                {
                    map.transform.parent = removedStack.transform;
                }
                map.transform.position = new Vector3(removedStackPosition.x, removedStackPosition.y + i * MapGenerator.MAPS_Y_OFFSET, removedStackPosition.z);
                map.transform.localScale = MAP_IN_DRAWER_SCALE;
                map.transform.localRotation = Quaternion.identity;
                i++;
            }
        }
        removedStack = null;
    }

    /**
     * randomly spreads given map on desk
     * map: game object to be spread
     */
    public static void spreadMapOnDesk(GameObject map)
    {
        float newX = DESK_FREE_AREA_LEFT_CORNER.x - Random.Range(0, DESK_FREE_AREA_LENGTH);
        float newZ = DESK_FREE_AREA_LEFT_CORNER.z + Random.Range(0, DESK_FREE_AREA_LENGTH);
        float newY = DESK_FREE_AREA_LEFT_CORNER.y + Random.Range(0, 0.01f);
        map.transform.position = (new Vector3(newX, newY, newZ));
        map.transform.localScale = MAP_ON_FREE_AREA_OF_DESK_SCALE;
    }
}
