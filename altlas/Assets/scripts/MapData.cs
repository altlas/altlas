using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[System.Serializable]
public class MapData
{
    public int m_year;
    public string m_language;
    public string m_coordinate;
    public string m_title;
    public string m_source;
    public int[] m_imageSize;
    public string m_property;
    public string m_description;
    public string m_category;
    public string m_subCategory;
    public string m_location;
    public string m_id;
    public Texture2D texture = null;
    public Texture2D thumbnail;

    public MapData(int year, string language, string coordinate,
                      string title, string source, int[] imageSize,
                      string property, string description,
                      string category, string subCategory,
                      string location, string id)
    {
        m_year = year;
        m_language = language;
        m_coordinate = coordinate;
        m_title = title;
        m_source = source;
        m_imageSize = imageSize;
        m_property = property;
        m_description = description;
        m_category = category;
        m_subCategory = subCategory;
        m_location = location;
        m_id = id;
        thumbnail = getTextureFromPic(idToTexturePath(id, category, subCategory, true));
    }

    public string idToTexturePath(string id, string category, string subcategory, bool getThumbnail = false)
    {
        string path = (getThumbnail ? "/models/maps/thumbnails/" : "/models/maps/") + category.ToLower();
        if (!category.Equals(subcategory))
        {
            path += "/" + subcategory.ToLower();
        }
        path += "/" + Regex.Replace(id, @"\s+", "") + ".jpg";
        return path;
    }

    public static Texture2D getTextureFromPic(string filePath)
    {
        Texture2D texture = null;
        byte[] fileData;
        string appFilePath = Application.dataPath + filePath;

        if (System.IO.File.Exists(appFilePath))
        {
            fileData = System.IO.File.ReadAllBytes(appFilePath);
            texture = new Texture2D(1, 1);
            texture.LoadImage(fileData);
        }
        return texture;
    }

    public void loadTexture()
    {
        texture = getTextureFromPic(idToTexturePath(m_id, m_category, m_subCategory));
    }

    public void disposeTexture()
    {
        texture = null;
    }

    public string userRelevantDataToString() {
        int lineLimit = 80;
        string result = "";
        string richTextStart = "<b><i>";
        string richTextEnd = "</i></b>";
        string[] userRelevantData = {m_title, m_location , m_year.ToString(), m_description, m_language, m_source};
        string attribut = null;

        for (int i = 0; i < userRelevantData.Length; i++) {
            result += richTextStart;
            switch (i) {
                case 0: attribut = "Titel: ";
                    break;
                case 1: attribut = "Ort: ";
                    break;
                case 2:
                    attribut = "Jahr: ";
                    break;
                case 3:
                    attribut = "Beschreibung: ";
                    break;
                case 4:
                    attribut = "Sprache: ";
                    break;
                case 5:
                    attribut = "Quelle: ";
                    break;
            }
            result += attribut + richTextEnd;
            int k = 0;
            for (; k < (userRelevantData[i].Length / lineLimit)-1; k++) {
                result += userRelevantData[i].Substring(k * lineLimit, lineLimit) + "\n";
            }
            if (userRelevantData[i].Length % lineLimit != 0)
            result += userRelevantData[i].Substring(k*lineLimit, (userRelevantData[i].Length % lineLimit));
            
            result += "\n";
        }
        return result;
           
    }
}
