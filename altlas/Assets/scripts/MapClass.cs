using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClass : MonoBehaviour {
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
    public Texture2D texture;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initiate(int year, string language, string coordinate,
        string title, string source, int[] imageSize, string property,
        string description, string category, string subCategory, string location, string id) {
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
        texture = getTextureFromPic(idToTexturePath(id, category, subCategory));
    }

    public void copyFrom(MapClass reference)
    {
        m_year = reference.m_year;
        m_language = reference.m_language;
        m_coordinate = reference.m_coordinate;
        m_title = reference.m_title;
        m_source = reference.m_source;
        m_imageSize = reference.m_imageSize;
        m_property = reference.m_property;
        m_description = reference.m_description;
        m_category = reference.m_category;
        m_subCategory = reference.m_subCategory;
        m_location = reference.m_location;
        m_id = reference.m_id;
        texture = reference.texture;
    }

    public string idToTexturePath(string id, string category, string subcategory) {
        string path = "/models/maps/"+ category.ToLower();
        if (!category.Equals(subcategory)) {
            path += "/" + subcategory.ToLower();
        }
        string picName = "/HK";
        string rawId = id.Substring(2);
        int realID = int.Parse(rawId);
        if (realID < 1000) {
            picName += "0";
        }
        picName += realID.ToString() + ".jpg";
        path += picName;
        Debug.Log(path);
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
            Debug.Log("I am Inside!");
        }

        Debug.Log(appFilePath);
        return texture;
    }

}
