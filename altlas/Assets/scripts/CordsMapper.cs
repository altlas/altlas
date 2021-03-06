using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordsMapper{
    public CordsMapper(){

    }
    public Vector3[,] GenerateCords(float[] cords, int dimension, float radius)
    {
        float longituteRange = (cords[1] - cords[0]) / dimension;
        float latitudeRange = (cords[3] - cords[2]) / dimension;
        Vector3[,] points = new Vector3[dimension + 1, dimension + 1];
        for (int i = 0; i <= dimension; i++)
        {
            for (int j = 0; j <= dimension; j++)
            {
                points[i, j] = GeneratePoint(cords[0] + j * longituteRange, cords[2] + i * latitudeRange, radius);
            }
        }
        return points;
    }
    public Vector3 GeneratePoint(float longitude, float latitude, float radius)
    {
        Vector3 vec = new Vector3(0f, 0f, radius);
        return Quaternion.AngleAxis(longitude, -Vector3.up) * Quaternion.AngleAxis(latitude, -Vector3.right) * vec;
    }
}
