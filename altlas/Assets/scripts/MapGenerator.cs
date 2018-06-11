using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
    private float DRAWER_HOR_LEN = 0.12f;
    private float DRAWER_VER_LEN = 0.54f;
    private float DRAWER_SHORT_X_DIS = 0.3f;
    private float DRAWER_LONG_X_DIS = 1.4f;
    private float DRAWER_Y_DIS = 0.24f;

    private float DRAWER_Z_POSITION = 0.37f;
    private float STARTING_DRAWER_X_POSITION = 0.70f;
    private float STARTING_DRAWER_Y_POSITION = 0.6f;
    private float MAPS_Y_OFFSET = 0.01f;

    /* TODO: make this to acceptable code and not hardcoded like this */

    List<MapClass> allMaps = new List<MapClass>();

    List<MapClass>[,] categories = {
        {new List<MapClass>(),null, null, null}, //astronomie
        {new List<MapClass>(),null, null, null}, //welkarten
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(),new List<MapClass>()}, //kontinentkarten, landkarten, stadtplan, inselkarte...
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(), null},
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(),new List<MapClass>()},
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(),new List<MapClass>()},
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(),new List<MapClass>()},
        {new List<MapClass>(), new List<MapClass>(), new List<MapClass>(), null},
        {new List<MapClass>(),null, null, null},
        {new List<MapClass>(),null, null, null},
        {new List<MapClass>(),null, null, null},
        {new List<MapClass>(), null, null, null }
    };

    Dictionary<string, Dictionary<string, List<MapClass>>> subCats = new Dictionary<string, Dictionary<string, List<MapClass>>>();

    /*Dictionary<string, List<List<MapClass>>> subCats = new Dictionary<string, List<List<MapClass>>> {
        { "astronomie", new List<List<MapClass>>{new List<MapClass>()}},
        { "astronomie", new List<List<MapClass>>{new List<MapClass>()}},
    };*/



    // Use this for initialization
    void Start() {

        MapClass map1 = new MapClass(1960, "language", "coordinate", "title", "source", new int[] { 21, 21 }, "property", "description", "astronomie", "astronomie", "location", "HK 1305");
        MapClass map2 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "bauplaene", "bauplaene", "location2", "HK 0188");
        MapClass map3 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapClass[] maps = { map1, map1, map2 };
        MapClass[][] maps2 = { maps, maps, maps, maps };
        for (int i = 0; i < 12; i++) {
            allMaps.Add(map1);
            allMaps.Add(map2);
            allMaps.Add(map3);
        }
        
        //Create Dic for every category / subcategory
        foreach (MapClass map in allMaps) {
            if (!subCats.ContainsKey(map.m_category)) {
                subCats.Add(map.m_category, new Dictionary<string, List<MapClass>>());
            }
            Dictionary<string, List<MapClass>> dicToAddTo = subCats[map.m_category];
            if (!subCats[map.m_category].ContainsKey(map.m_subCategory))
            {
                dicToAddTo.Add(map.m_subCategory, new List<MapClass>());
            }
            List<MapClass> listToAddTo = dicToAddTo[map.m_subCategory];
            dicToAddTo[map.m_subCategory].Add(map);
        }


        foreach (KeyValuePair<string, Dictionary<string, List<MapClass>>> domCat in subCats)
        {
            // do something with entry.Value or entry.Key
            List<List<MapClass>> catData = new List<List<MapClass>>();
            foreach (KeyValuePair<string, List<MapClass>> subCat in subCats[domCat.Key])
            {
                // do something with entry.Value or entry.Key
                catData.Add(subCat.Value);
            }
            spawnRowInDrawer(catData, domCat.Key);
        }

    } 

    /**
     * only use this for single placements 
     * */
    private void spawnMap(MapClass mapData, string category)
    {
        var MapToSpawn = Instantiate(map,getDrawerVectorByCategory(category), Quaternion.identity).GetComponent<MapClass>();
        MapToSpawn.copyFrom(mapData);
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    public void spawnMap(Vector3 position, MapClass mapData, string category)
    {
        var MapToSpawn = Instantiate(map, position, Quaternion.identity).GetComponent<MapClass>();
        MapToSpawn.copyFrom(mapData);
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    public void spawnAsStack(Vector3 position, MapClass[] mapData, string category) {
        for (int i = 0; i < mapData.Length; i++)
        {
            position.y = position.y + MAPS_Y_OFFSET;
            spawnMap(position, mapData[i], category);
        }
    }

    public void spawnAsStack(Vector3 position, List<MapClass> mapData, string category)
    {
        foreach (MapClass map in mapData)
        {
            position.y = position.y + MAPS_Y_OFFSET;
            spawnMap(position, map, category);
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

    public void spawnStacksInRow(float horLength, float verLength, List<List<MapClass>> stacks, string category)
    {
        float x = getDrawerVectorByCategory(category).x;
        float z = getDrawerVectorByCategory(category).z;
        float y = getDrawerVectorByCategory(category).y;
        int i = 0;
        foreach (List<MapClass> domCategory in stacks)
        {
            float newX = x - horLength / 2;
            float newZ = z + (stacks.Count - i) * verLength / stacks.Count;
            spawnAsStack(new Vector3(newX, y, newZ), stacks[i], category);
            i++;
        }
    }

    public void spawnStacksInRow(float horLength, float verLength, List<MapClass>[] stacks, string category)
    {
        float x = getDrawerVectorByCategory(category).x;
        float z = getDrawerVectorByCategory(category).z;
        float y = getDrawerVectorByCategory(category).y;
        for (int i = 0; i < stacks.GetLength(0); i++)
        {
            float newX = x - horLength / 2;
            float newZ = z + (stacks.GetLength(0) - i) * verLength / stacks.GetLength(0);
            spawnAsStack(new Vector3(newX, y, newZ), stacks[i], category);
            i++;
        }
    }

    public void spawnRowInDrawer(MapClass[][] stacks, string category) {
        int x = getDrawerIndexByCategory(category)[0];
        int y= getDrawerIndexByCategory(category)[1];
        spawnStacksInRow(DRAWER_HOR_LEN, DRAWER_VER_LEN, stacks, category);
    }

    public void spawnRowInDrawer(List<List<MapClass>> stacks, string category)
    {
        int x = getDrawerIndexByCategory(category)[0];
        int y = getDrawerIndexByCategory(category)[1];
        spawnStacksInRow(DRAWER_HOR_LEN, DRAWER_VER_LEN, stacks, category);
    }

    public void spawnRowInDrawer(List<MapClass>[] stacks, string category)
    {
        int x = getDrawerIndexByCategory(category)[0];
        int y = getDrawerIndexByCategory(category)[1];
        spawnStacksInRow(DRAWER_HOR_LEN, DRAWER_VER_LEN, stacks, category);
    }



    /**
     * x e {0,3}
     * y e {0,2}
     * */
    public Vector3 getDrawerVectorByIndex(int x, int y) {
        float newX = STARTING_DRAWER_X_POSITION - (x % 2) * DRAWER_SHORT_X_DIS;
        float newY = STARTING_DRAWER_Y_POSITION - y * DRAWER_Y_DIS;
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
            case "politische_oekonomische_regionen":
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
                case "politische_oekonomische_regionen":
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
