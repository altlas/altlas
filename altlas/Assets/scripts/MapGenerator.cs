using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
    private float DRAWER_HOR_LEN = 0.36f;
    private float DRAWER_VER_LEN = 0.44f;

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

    // Use this for initialization
    void Start () {


        MapClass map1 = new MapClass(1960, "language", "coordinate", "title", "source", new int[] { 21, 21 }, "property", "description", "astronomie", "astronomie", "location", "HK 1305");
        MapClass map2 = new MapClass(1960, "language2", "coordinate2", "title2", "source2", new int[] { 21, 21 }, "property2", "description2", "geografische_regionen", "landkarten", "location2", "HK 0188");
        MapClass[] maps = { map1, map1, map2 };
        MapClass[][] maps2 = { maps, maps, maps};
        spawnStacksInCorner(new Vector3(-0.7105434f, 1.267f, 0.4124395f), DRAWER_HOR_LEN, DRAWER_VER_LEN, maps2);
    }

    public void spawnInArea(Vector3 leftCorner,MapClass[] maps, float horLength, float verLength, float xOffset, float zOffset) {
        float x = leftCorner.x;
        float z = leftCorner.z;
        float y = leftCorner.y;

        for (int i = 0; i < maps.Length; i++) {
            float newX = x - Random.Range(xOffset, horLength);
            float newZ = z + Random.Range(zOffset, verLength);
            spawnMap(new Vector3(newX, y, newZ), maps[i]);
        }
    }

    public void spawnMap(Vector3 position, MapClass mapData)
    {
        var MapToSpawn = Instantiate(map, position, Quaternion.identity).GetComponent<MapClass>();
        MapToSpawn.copyFrom(mapData);
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
    }

    public void spawnAsStack(Vector3 position, MapClass[] mapData) {
        for (int i = 0; i < mapData.Length; i++)
        {
            spawnMap(position, mapData[i]);
        }
    }

    public void spawnStacksInCorner(Vector3 leftCorner, float horLength, float verLength, MapClass[][] stacks) {
        float x = leftCorner.x;
        float z = leftCorner.z;
        float y = leftCorner.y;
        for (int i = 0; i < 2; i++) {
            for (int k = 0; k < 2; k++) {
                if (2 * i + k < stacks.Length) {
                    float newX = x - i * horLength;
                    float newZ = z + k * verLength;
                    spawnAsStack(new Vector3(newX, y, newZ), stacks[2 * i + k]);
                }
            }
        }
    }

}
