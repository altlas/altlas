using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
    private float DRAWER_HOR_LEN = 0.12f;
    private float DRAWER_VER_LEN = 0.54f;
    private float DRAWER_SHORT_X_DIS = 0.3f;
    private float DRAWER_LONG_X_DIS = 1.4f;
    private float DRAWER_Y_DIS = 0.0024f;

    private float DRAWER_Z_POSITION = 0.37f;
    private float STARTING_DRAWER_X_POSITION = 0.70f;
    private float STARTING_DRAWER_Y_POSITION = 0.8f;

    List<MapClass> allMaps = new List<MapClass>();
    List<MapClass> astronomie = new List<MapClass>();
    List<MapClass> weltkarten = new List<MapClass>();
    List<MapClass> geografisch = new List<MapClass>();
    List<MapClass> physisch = new List<MapClass>();
    List<MapClass> geologisch = new List<MapClass>();
    List<MapClass> gewaesser = new List<MapClass>();
    List<MapClass> politisch = new List<MapClass>();
    List<MapClass> infrastruktur = new List<MapClass>();
    List<MapClass> forschungsreisen = new List<MapClass>();
    List<MapClass> kolonie = new List<MapClass>();
    List<MapClass> geschichte = new List<MapClass>();
    List<MapClass> bauplaene = new List<MapClass>();

    string[] category = new string[12] {
        "astronomie",
        "weltkarten",
        "geografische_regionen",
        "physisch",
        "geologisch",
        "gewaesser",
        "politische_oekonomische_regionen",
        "infrastruktur",
        "forschungsreisen",
        "koloniekarte",
        "geschichtskarte",
        "bauplaene"
    };

    

    // Use this for initialization
    void Start () {


        MapClass map1 = new MapClass(1960, "language", "coordinate", "title", "source", new int[] { 21, 21 }, "property", "description", "astronomie", "astronomie", "location", "HK 1305");
        MapClass map2 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapClass[] maps = { map1, map1, map2 };
        MapClass[][] maps2 = { maps, maps, maps, maps};
        for (int i = 0; i< 12; i++) 
        spawnMap(map1, category[i]);
        //spawnStacksInRow(new Vector3(-0.007092f - DRAWER_X_OFFSET, 1.267f, 0.005999998f + DRAWER_Z_OFFSET), DRAWER_HOR_LEN, DRAWER_VER_LEN, maps2);

        /*for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++) {
                spawnRowInDrawer(maps2, category[2*x +y]);
            }
        }*/
        //spawnRowInDrawer(maps2, "astronomie");

    } 

    public void spawnMap(MapClass mapData, string category)
    {
        var MapToSpawn = Instantiate(map,getDrawerVectorByCategory(category), Quaternion.identity).GetComponent<MapClass>();
        MapToSpawn.copyFrom(mapData);
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    public void spawnMap(Vector3 position, MapClass mapData, string category)
    {
        var MapToSpawn = Instantiate(map, getDrawerVectorByCategory(category), Quaternion.identity).GetComponent<MapClass>();
        MapToSpawn.copyFrom(mapData);
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    public void spawnAsStack(Vector3 position, MapClass[] mapData, string category) {
        for (int i = 0; i < mapData.Length; i++)
        {
            spawnMap(position, mapData[i], category);
        }
    }

    public Vector3 getDrawerVectorByCategory(string category) {
        return getDrawerVectorByIndex(getDrawerIndexByCategory(category)[0], getDrawerIndexByCategory(category)[1]);
    }

    public void spawnStacksInRow(float horLength, float verLength, MapClass[][] stacks, string category) {
        float x = getDrawerVectorByCategory(category).x;
        float z = getDrawerVectorByCategory(category).z;
        float y = getDrawerVectorByCategory(category).y;
        for (int i = 0; i < stacks.Length; i++) {
            float newX = x - horLength/2;
            float newZ = z + (stacks.Length-i)* verLength/stacks.Length;
            spawnAsStack(new Vector3(newX, y, newZ), stacks[i], category);
        }
    }

    public void spawnRowInDrawer(MapClass[][] stacks, string category) {
        int x = getDrawerIndexByCategory(category)[0];
        int y= getDrawerIndexByCategory(category)[1];
        spawnStacksInRow(DRAWER_HOR_LEN, DRAWER_VER_LEN, stacks, category);
    }

    /**
     * x e {0,3}
     * y e {0,2}
     * */
    public Vector3 getDrawerVectorByIndex(int x, int y) {
        float newX = STARTING_DRAWER_X_POSITION - (x % 2) * DRAWER_SHORT_X_DIS;
        float newY = STARTING_DRAWER_Y_POSITION - y * DRAWER_SHORT_X_DIS;
        if (x > 1)
            newX -= DRAWER_LONG_X_DIS;
        return new Vector3(newX, newY, DRAWER_Z_POSITION);
    }

    public GameObject getDrawerObjectFromCategory(string category) {
        switch (category) {
            case "astronomie":
                return GameObject.Find("00");
            case "weltkarten":
                return GameObject.Find("10");
            case "geografische_regionen":
                return GameObject.Find("20");
            case "physisch":
                return GameObject.Find("30");
            case "geologisch":
                return GameObject.Find("01");
            case "gewaesser":
                return GameObject.Find("11");
            case "apolitisch_oekonomische_regionen":
                return GameObject.Find("21");
            case "infrastruktur":
                return GameObject.Find("31");
            case "forschungsreisen":
                return GameObject.Find("02");
            case "koloniekarte":
                return GameObject.Find("12");
            case "geschichtskarte":
                return GameObject.Find("22");
            case "bauplaene":
                return GameObject.Find("32");
        }
        return null;
    }

    public int[] getDrawerIndexByCategory(string category) {
            switch (category)
            {
                case "astronomie":
                    return new int[] { 0,0 };
                case "weltkarten":
                return new int[] { 1, 0 };
                case "geografische_regionen":
                return new int[] { 2, 0 };
                case "physisch":
                return new int[] { 3, 0 };
                case "geologisch":
                return new int[] { 0, 1 };
                case "gewaesser":
                return new int[] { 1, 1 };
                case "apolitisch_oekonomische_regionen":
                return new int[] { 2, 1 };
                case "infrastruktur":
                return new int[] { 3, 1 };
                case "forschungsreisen":
                return new int[] { 0, 2 };
                case "koloniekarte":
                return new int[] { 1, 2 }; 
                case "geschichtskarte":
                return new int[] { 2, 2 };
                case "bauplaene":
                return new int[] { 3, 2 };
        }
            return null;
    }

}
