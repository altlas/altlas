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
        float upperLeftLongitute = 0;
        float upperLeftLatitute = 0;
        float lowerRightLongitute = 0;
        float lowerRightLatitute = 0;
        foreach (Match m in Regex.Matches(cords, pattern))
        {
            int value = int.Parse(m.Groups[1].Value);
            switch (i)
            {
                case 0:
                    upperLeftLongitute += value;
                    break;
                case 1:
                    upperLeftLongitute += (float)value / 60;
                    break;
                case 2:
                    upperLeftLongitute += (float)value / 3600;
                    break;
                case 3:
                    lowerRightLongitute += value;
                    break;
                case 4:
                    lowerRightLongitute += (float)value / 60;
                    break;
                case 5:
                    lowerRightLongitute += (float)value / 3600;
                    break;
                case 6:
                    upperLeftLatitute += value;
                    break;
                case 7:
                    upperLeftLatitute += (float)value / 60;
                    break;
                case 8:
                    upperLeftLatitute += (float)value / 3600;
                    break;
                case 9:
                    lowerRightLatitute += value;
                    break;
                case 10:
                    lowerRightLatitute += (float)value / 60;
                    break;
                case 11:
                    lowerRightLatitute += (float)value / 3600;
                    break;
            }
            i++;
        }
        if (cords[0] == 'W')
        {
            upperLeftLongitute = -upperLeftLongitute;
        }
        if (cords[12] == 'W')
        {
            lowerRightLongitute = -lowerRightLongitute;
        }
        if (cords[24] == 'S')
        {
            upperLeftLatitute = -upperLeftLatitute;
        }
        if (cords[36] == 'S')
        {
            lowerRightLatitute = -lowerRightLatitute;
        }
        return new float[] { upperLeftLongitute, lowerRightLongitute, upperLeftLatitute, lowerRightLatitute };
    }
}
