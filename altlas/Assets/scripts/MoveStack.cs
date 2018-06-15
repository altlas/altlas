using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveStack {
    public static Vector3 DESK_FREE_AREA_LEFT_CORNER = new Vector3(-0.7105434f, 0.85f, 0.4124395f);
    public static Vector3 MAP_IN_DRAWER_SCALE = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Map_alt.prefab", typeof(GameObject))).transform.localScale;
    public static float MAP_ON_FREE_AREA_OF_DESK_SCALE = .2f;
    private static float MAP_ON_MIDDLE_OF_DESK_SCALE = .6f;
    public static Vector3 MIDDLE_OF_DESK = new Vector3(-0.2f, 0.858f, 0.65f); //absolute positon
    
    public static GameObject removedStack = null;
    public static string MAPTAG = "Pickupable"; 
    public static float DESK_FREE_AREA_LENGTH = 0.4f;
    public static string textDisplayName = "map-information";
    public static GameObject MAP_ON_MIDDLE_OF_DESK = null;


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
        string mapNamesOfStackToRemove = removedStack.transform.name.Remove(removedStack.transform.name.Length - MapGenerator.EMPTY_ENDING.Length);
        Vector3 removedStackPosition = new Vector3(removedStack.transform.position.x, removedStack.transform.position.y, removedStack.transform.position.z);
        int i = 0;
        foreach (GameObject map in GameObject.FindGameObjectsWithTag(MAPTAG))
        {
            if (map.name.Equals(mapNamesOfStackToRemove))
            {
                if (map.transform.parent == null)
                {
                    map.transform.parent = removedStack.transform;
                }
                map.transform.position = new Vector3(removedStackPosition.x, removedStackPosition.y + i * MapGenerator.MAPS_Y_OFFSET, removedStackPosition.z);
                map.transform.localScale = MAP_IN_DRAWER_SCALE;
                map.transform.localRotation = Quaternion.identity;
                map.GetComponent<MapOnClick>().state = MapOnClick.MapState.InDrawer;
                i++;
            }
        }
        removedStack = null;
        MAP_ON_MIDDLE_OF_DESK = null;
        GameObject.Find(textDisplayName).GetComponent<TextMesh>().text = "Select a map!";
    }

    /**
     * randomly spreads given map on desk
     * map: game object to be spread
     */
    public static void spreadMapOnDesk(GameObject map)
    {
        var mapData = map.GetComponent<MapScript>().data;
        var imageSize = mapData.m_imageSize;

        map.GetComponent<MapOnClick>().state =  MapOnClick.MapState.OnSideOfDesk;

        float newX = DESK_FREE_AREA_LEFT_CORNER.x - Random.Range(0, DESK_FREE_AREA_LENGTH);
        float newZ = DESK_FREE_AREA_LEFT_CORNER.z + Random.Range(0, DESK_FREE_AREA_LENGTH);
        float newY = DESK_FREE_AREA_LEFT_CORNER.y + Random.Range(0, 0.01f);
        map.transform.position = (new Vector3(newX, newY, newZ));
        var y = map.transform.localScale.y;
        var z = MAP_ON_FREE_AREA_OF_DESK_SCALE;
        var x = z * imageSize[1] / imageSize[0];
        map.transform.localScale = new Vector3(x, y, z);// MAP_ON_FREE_AREA_OF_DESK_SCALE;
    }

    public static void moveMapToMiddleOfDesk(GameObject map) {
        //there already is a map on the middle of the desk, so first change its position
        if (MAP_ON_MIDDLE_OF_DESK != null)
        {
            var mapData = map.GetComponent<MapScript>().data;
            var imageSize = mapData.m_imageSize;

            var y = map.transform.localScale.y;
            var z = MAP_ON_MIDDLE_OF_DESK_SCALE;
            var x = z * imageSize[1] / imageSize[0];

            MAP_ON_MIDDLE_OF_DESK.transform.position = map.transform.position;
            MAP_ON_MIDDLE_OF_DESK.transform.localScale = new Vector3(x, y, z);
        }
        MAP_ON_MIDDLE_OF_DESK = map.gameObject;
        GameObject.Find(textDisplayName).GetComponent<TextMesh>().text = MAP_ON_MIDDLE_OF_DESK.GetComponent<MapScript>().data.userRelevantDataToString();
    }

}