using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
    private Loader loader;
    private bool once = true;

    //SCENE CONSTANTS FOR PLACING
    private float DRAWER_HOR_LEN = 0.12f;
    private float DRAWER_VER_LEN = 0.54f;
    private float DRAWER_SHORT_X_DIS = 0.3f;
    private float DRAWER_LONG_X_DIS = 1.4f;
    private float DRAWER_Y_DIS = 0.24f;

    private float DRAWER_Z_POSITION = 0.37f;
    private float STARTING_DRAWER_X_POSITION = 0.70f;
    private float STARTING_DRAWER_Y_POSITION = 0.6f;
    private float MAPS_Y_OFFSET = 0.01f;

    private Vector3 DESK_FREE_AREA_LEFT_CORNER = new Vector3(-0.7105434f, 0.85f, 0.4124395f);
    private float DESK_FREE_AREA_LENGTH = 0.4f;

    //List<MapData> allMaps = new List<MapData>();

    Dictionary<string, Dictionary<string, List<MapData>>> subCats = new Dictionary<string, Dictionary<string, List<MapData>>>();
    // Use this for initialization
    void Start() {
        loader = GetComponent<Loader>();

        // TESTING //
        MapData map1 = new MapData(1960, "language", "coordinate", "title", "source", new int[] { 21, 21 }, "property", "description", "astronomie", "astronomie", "location", "HK 1305");
        MapData map2 = new MapData(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "bauplaene", "bauplaene", "location2", "HK 0188");
        MapData map3 = new MapData(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapData map4 = new MapData(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "inselkarte", "location2", "HK 0188");
        MapData map5 = new MapData(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "kontinent", "location2", "HK 0188");
        MapData map6 = new MapData(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "stadtplan", "location2", "HK 0188");
        MapData[] maps = { map1, map1, map2 };
        MapData[][] maps2 = { maps, maps, maps, maps };
        /*for (int i = 0; i < 7; i++) {
            allMaps.Add(map1);
            allMaps.Add(map2);
            allMaps.Add(map3);
            allMaps.Add(map4);
            allMaps.Add(map5);
            allMaps.Add(map6);
        }*/

        sortMaps();
        spawnInArea(DESK_FREE_AREA_LEFT_CORNER, maps, DESK_FREE_AREA_LENGTH, DESK_FREE_AREA_LENGTH);

        foreach (KeyValuePair<string, Dictionary<string, List<MapData>>> domCat in subCats)
        {
            List<List<MapData>> catData = new List<List<MapData>>();
            foreach (KeyValuePair<string, List<MapData>> subCat in subCats[domCat.Key])
            {
                catData.Add(subCat.Value);
            }
            spawnRowInDrawer(catData, domCat.Key);
        }

    }

    /*void Update()
    {
        if (once && loader.finishedLoading)
        {
            once = false;
            foreach (MapData lmap in loader.data)
            {
                spawnMap(new Vector3(-0.7105434f, 1.267f, 0.4124395f), lmap);
            }
        }
    }*/

    /**
     * initializes subCats
    */
    private void sortMaps() {
        if (once && loader.finishedLoading)
        {
            // Create Dic for every category / subcategory
            foreach (MapData map in loader.data)
            {
                if (!subCats.ContainsKey(map.m_category))
                {
                    subCats.Add(map.m_category, new Dictionary<string, List<MapData>>());
                }
                Dictionary<string, List<MapData>> dicToAddTo = subCats[map.m_category];
                if (!subCats[map.m_category].ContainsKey(map.m_subCategory))
                {
                    dicToAddTo.Add(map.m_subCategory, new List<MapData>());
                }
                List<MapData> listToAddTo = dicToAddTo[map.m_subCategory];
                dicToAddTo[map.m_subCategory].Add(map);
            }
        }
    }
    /**
     * Spawns map at given position without being bound to the movement of a drawer
     * position: the position the map object should be spawned in the scene
     * mapData: the data the map object will have
     * */
    private void spawnMap(Vector3 position, MapData mapData)
    {
        var MapToSpawn = Instantiate(map, position, Quaternion.identity).GetComponent<MapScript>();
        MapToSpawn.data = mapData;
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
    }

    /**
     * Spawns map in drawer which its category is assigned to. use this method for single placements of a map
     * mapData: the data the map object will have
     * category: the drawer which contains all maps of this category 
     */
    private void spawnMap(MapData mapData, string category)
    {
        var MapToSpawn = Instantiate(map,getDrawerVectorByCategory(category), Quaternion.identity).GetComponent<MapScript>();
        MapToSpawn.data = mapData;
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    /**
     * Spawns map in drawer which its category is assigned to. use this method if you want to spread map objects over a certain area
     * position: the position the map object should be spawned in the scene
     * mapData: the data the map object will have
     * category: the drawer which contains all maps of this category.
     */
    public void spawnMap(Vector3 position, MapData mapData, string category)
    {
        var MapToSpawn = Instantiate(map, position, Quaternion.identity).GetComponent<MapScript>();
        MapToSpawn.data = mapData;
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
        MapToSpawn.transform.parent = getDrawerObjectFromCategory(category).transform;
    }

    /**
     * randomly spreads given maps over a certain 2D area = horLength * verLength
     * leftCorner: left corner of area
     * maps: the data the map objects will have
     * horLength: width of area
     * verLength: height of area
     */
    public void spawnInArea(Vector3 leftCorner, MapData[] maps, float horLength, float verLength)
    {
        float x = leftCorner.x;
        float z = leftCorner.z;
        float y = leftCorner.y;

        for (int i = 0; i < maps.Length; i++)
        {
            float newX = x - Random.Range(0, horLength);
            float newZ = z + Random.Range(0, verLength);
            spawnMap(new Vector3(newX, y, newZ), maps[i]);
        }
    }

    /**
     * randomly spreads given maps over a certain 2D area = horLength * verLength
     * leftCorner: left corner of area
     * maps: the data the map objects will have
     * horLength: width of area
     * verLength: height of area
     */
    public void spawnInArea(Vector3 leftCorner, List<MapData> maps, float horLength, float verLength)
    {
        float x = leftCorner.x;
        float z = leftCorner.z;
        float y = leftCorner.y;

        foreach (MapData map in maps)
        {
            float newX = x - Random.Range(0, horLength);
            float newZ = z + Random.Range(0, verLength);
            spawnMap(new Vector3(newX, y, newZ), map);
        }
    }

    /**
     * Will spawn given maps as a stack at a given position.
     * position: the position the map object should be spawned in the scene
     * mapData: the data the map object will have
     * category: the drawer which contains all maps of this category.
     * */
    public void spawnAsStack(Vector3 position, MapData[] mapData, string category) {
        for (int i = 0; i < mapData.Length; i++)
        {
            position.y = position.y + MAPS_Y_OFFSET;
            spawnMap(position, mapData[i], category);
        }
    }

    /**
     * Will spawn given maps as a stack in the drawer
     * position: the position the map object should be spawned in the scene
     * mapData: the data the map object will have
     * category: the drawer which contains all maps of this category
     * */
    public void spawnAsStack(Vector3 position, List<MapData> mapData, string category)
    {
        foreach (MapData map in mapData)
        {
            position.y = position.y + MAPS_Y_OFFSET;
            spawnMap(position, map, category);
        }
    }

    /**
     * Will spawn multiple map stacks aligned in a row 
     * horLength: width of area
     * verLength: height of area
     * stacks: stacks of maps to spawn, first dimension: category, second: subcategories belonging to category
     * category: drawer which its category is assigned to where row should spawn 
     * */
    public void spawnStacksInRow(float horLength, float verLength, MapData[][] stacks, string category) {
        float x = getDrawerVectorByCategory(category).x;
        float z = getDrawerVectorByCategory(category).z;
        float y = getDrawerVectorByCategory(category).y;
        for (int i = 0; i < stacks.Length; i++) {
            float newX = x - horLength/2;
            float newZ = z + (stacks.Length-i)* verLength/stacks.Length;
            spawnAsStack(new Vector3(newX, y, newZ), stacks[i], category);
        }
    }

    /**
     * Will spawn multiple map stacks aligned in a row 
     * horLength: width of area
     * verLength: height of area
     * stacks: stacks of maps to spawn, first dimension: category, second: subcategories belonging to category
     * category: drawer which its category is assigned to where row should spawn 
     * */
    public void spawnStacksInRow(float horLength, float verLength, List<List<MapData>> stacks, string category)
    {
        float x = getDrawerVectorByCategory(category).x;
        float z = getDrawerVectorByCategory(category).z;
        float y = getDrawerVectorByCategory(category).y;
        int i = 0;
        foreach (List<MapData> domCategory in stacks)
        {
            float newX = x - horLength / 2;
            float newZ = z + (stacks.Count - i) * verLength / stacks.Count;
            spawnAsStack(new Vector3(newX, y, newZ), stacks[i], category);
            i++;
        }
    }

    /**
     * spawnStackInRow with fixed drawer length
     * */
    public void spawnRowInDrawer(MapData[][] stacks, string category) {
        int x = getDrawerIndexByCategory(category)[0];
        int y= getDrawerIndexByCategory(category)[1];
        spawnStacksInRow(DRAWER_HOR_LEN, DRAWER_VER_LEN, stacks, category);
    }

    /**
     * spawnStackInRow with fixed drawer length
     * */
    public void spawnRowInDrawer(List<List<MapData>> stacks, string category)
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

    public Vector3 getDrawerVectorByCategory(string category) {
        return getDrawerVectorByIndex(getDrawerIndexByCategory(category)[0], getDrawerIndexByCategory(category)[1]);
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
