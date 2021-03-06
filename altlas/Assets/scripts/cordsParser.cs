using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CordsParser
{
    public CordsParser(){

    }

    public float[] parse(string cords)
    {
        string pattern = @"(\d+)";
        int i = 0;
        float upperLeftLatitude = 0;
        float upperLeftLongitute = 0;
        float lowerRightLatitude = 0;
        float lowerRightLongitute = 0;
        foreach (Match m in Regex.Matches(cords, pattern))
        {
            int value = int.Parse(m.Groups[1].Value);
            switch (i)
            {
                case 0:
                    upperLeftLatitude += value;
                    break;
                case 1:
                    upperLeftLatitude += (float)value / 60;
                    break;
                case 2:
                    upperLeftLatitude += (float)value / 3600;
                    break;
                case 3:
                    lowerRightLatitude += value;
                    break;
                case 4:
                    lowerRightLatitude += (float)value / 60;
                    break;
                case 5:
                    lowerRightLatitude += (float)value / 3600;
                    break;
                case 6:
                    upperLeftLongitute += value;
                    break;
                case 7:
                    upperLeftLongitute += (float)value / 60;
                    break;
                case 8:
                    upperLeftLongitute += (float)value / 3600;
                    break;
                case 9:
                    lowerRightLongitute += value;
                    break;
                case 10:
                    lowerRightLongitute += (float)value / 60;
                    break;
                case 11:
                    lowerRightLongitute += (float)value / 3600;
                    break;
            }
            i++;
        }
        if (cords[0] == 'W')
        {
            upperLeftLatitude = -upperLeftLatitude;
        }
        if (cords[12] == 'W')
        {
            lowerRightLatitude = -lowerRightLatitude;
        }
        if (cords[24] == 'S')
        {
            upperLeftLongitute = -upperLeftLongitute;
        }
        if (cords[36] == 'S')
        {
            lowerRightLongitute = -lowerRightLongitute;
        }
        return new float[] { upperLeftLongitute, lowerRightLongitute, upperLeftLatitude, lowerRightLatitude };
    }
}
