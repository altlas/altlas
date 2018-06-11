using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform map;
    private float DRAWER_HOR_LEN = 0.36f;
    private float DRAWER_VER_LEN = 0.44f;
    private Loader loader;
    private bool once = true;

    void Start () {
        loader = GetComponent<Loader>();
    }

    void Update(){
        if(once && loader.finishedLoading){
            once = false;
            foreach(MapData lmap in loader.data){
                spawnMap(new Vector3(-0.7105434f, 1.267f, 0.4124395f), lmap);
            }
        }
    }

    public void spawnMap(Vector3 position, MapData mapData)
    {   
        var MapToSpawn = Instantiate(map, position, Quaternion.identity).GetComponent<MapScript>();
        MapToSpawn.data = mapData;
        MapToSpawn.GetComponent<Renderer>().material.mainTexture = mapData.texture;
    }
}
