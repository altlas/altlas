using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        float[] inputdata = new float[]{ 0f, 30f, 16f, -12.6f };
        GenerateCords(inputdata, 3);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void GenerateCords(float[] cords, int dimension)
    {
        float longituteRange = (cords[1] - cords[0]) / dimension;
        float latitudeRange = (cords[3] - cords[2]) / dimension;
        Vector3[,] points = new Vector3[dimension + 1, dimension + 1];
        for (int i = 0; i <= dimension; i++)
        {
            for (int j = 0; j <= dimension; j++)
            {
                points[i, j] = GeneratePoint(cords[0] + j * longituteRange, cords[2] + i * latitudeRange);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                float scaleFactor =  .02f;
                cube.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                cube.transform.position = points[i, j];
            }
        }

    }
    Vector3 GeneratePoint(float longitude, float latitude)
    {
        return Quaternion.AngleAxis(longitude, -Vector3.up) * Quaternion.AngleAxis(latitude, -Vector3.right) * new Vector3(0, 0, .5f);
    }
}
