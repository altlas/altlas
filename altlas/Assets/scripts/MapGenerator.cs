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
    private float DRAWER_Y_DIS = 0.0024f;

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

    int[] numbOfSubs = { 1, 1, 4, 3, 3, 4, 4, 3 ,1,1,1,1};
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

   string[][] subCategoryInside = new string[12][]{
        new string[]{ "" },
        new string[]{ "" },
        new string[]{ "kontinentkarten", "landkarten", "stadtplan", "inselkarte" },
        new string[]{ "vulkankarte", "hochgebirge", "gebirge" },
        new string[]{ "topographische_karte", "bodenkarten", "relief" },
        new string[]{ "meerarten", "flusskarte", "hafenkarte", "kueste" },
        new string[]{ "verwaltungskarte", "politische_karte", "Katasterpläne", "kreiskarte" },
        new string[]{ "verkehrskarten", "eisenbahn", "militaerkartographie" },
        new string[]{ "" },
        new string[]{ "" },
        new string[]{ "" },
        new string[]{ "" }
    };


    Dictionary<string, Dictionary<string, List<MapClass>>> sortedMaps = new Dictionary<string, Dictionary<string, List<MapClass>>>();

   



    // Use this for initialization
    void Start() {

        MapClass map1 = new MapClass(1960, "language", "coordinate", "title", "source", new int[] { 21, 21 }, "property", "description", "astronomie", "astronomie", "location", "HK 1305");
        MapClass map2 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapClass map3 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapClass[] maps = { map1, map1, map2 };
        MapClass[][] maps2 = { maps, maps, maps, maps };
        for (int i = 0; i < 6; i++) {
            allMaps.Add(map1);
            allMaps.Add(map2);
        }
            //spawnRowInDrawer(maps2, category[i]); 

        foreach (MapClass map in allMaps) {
            switch (map.m_category) {
                case "astronomie":
                    categories[0,0].Add(map);
                    break;
                case "weltkarten":
                    categories[1,0].Add(map);
                    break;
                case "geografische_regionen":
                    switch (map.m_subCategory) {
                        case "kontinentkarten":
                            categories[2,0].Add(map);
                            break;
                        case "landkarten":
                            categories[2,1].Add(map);
                            break;
                        case "stadtplan":
                            categories[2,2].Add(map);
                            break;
                        case "inselkarte":
                            categories[2,3].Add(map);
                            break;
                    }
                    break;
                case "physisch":
                    switch (map.m_subCategory)
                    {
                        case "vulkankarte":
                            categories[3,0].Add(map);
                            break;
                        case "hochgebirge":
                            categories[3,1].Add(map);
                            break;
                        case "gebirge":
                            categories[3,2].Add(map);
                            break;
                    }
                    break;
                case "geologisch":
                    switch (map.m_subCategory)
                    {
                        case "topographische_karte":
                            categories[4,0].Add(map);
                            break;
                        case "bodenkarten":
                            categories[4,1].Add(map);
                            break;
                        case "relief":
                            categories[4,2].Add(map);
                            break;
                    }
                    break;
                case "gewaesser":
                    switch (map.m_subCategory)
                    {
                        case "meerarten":
                            categories[5,0].Add(map);
                            break;
                        case "flusskarte":
                            categories[5,1].Add(map);
                            break;
                        case "hafenkarte":
                            categories[5,2].Add(map);
                            break;
                        case "kueste":
                            categories[5,3].Add(map);
                            break;
                    }
                    break;
                case "politische_oekonomische_regionen":
                    switch (map.m_subCategory)
                    {
                        case "verwaltungskarte":
                            categories[6,0].Add(map);
                            break;
                        case "politische_karte":
                            categories[6,1].Add(map);
                            break;
                        case "Katasterpläne":
                            categories[6,2].Add(map);
                            break;
                        case "kreiskarte":
                            categories[6,3].Add(map);
                            break;
                    }
                    break;
                case "infrastruktur":
                    switch (map.m_subCategory)
                    {
                        case "verkehrskarten":
                            categories[7,0].Add(map);
                            break;
                        case "eisenbahn":
                            categories[7,1].Add(map);
                            break;
                        case "militaerkartographie":
                            categories[7,2].Add(map);
                            break;
                    }
                    break;
                case "forschungsreisen":
                    categories[8,0].Add(map);
                    break;
                case "koloniekarte":
                    categories[9,0].Add(map);
                    break;
                case "geschichtskarte":
                    categories[10,0].Add(map);
                    break;
                case "bauplaene":
                    categories[11,0].Add(map);
                    break;
            }
        }
        for (int i = 0; i < categories.GetLength(0); i++) {
            List<List<MapClass>> catData = new List<List<MapClass>>();
            for (int k = 0; k < categories.GetLength(1); k++) {
                if (categories[i, k] != null) {
                    catData.Add(categories[i,k]);
                }
            }
            spawnRowInDrawer(catData, category[i]);
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
