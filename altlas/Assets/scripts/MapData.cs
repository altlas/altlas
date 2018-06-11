﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MapData{
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

  public MapData(int year, string language, string coordinate, 
                    string title, string source, int[] imageSize, 
                    string property, string description, 
                    string category, string subCategory, 
                    string location, string id) {
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

  public string idToTexturePath(string id, string category, string subcategory) {
    string path = "/models/maps/"+ category.ToLower();
    if (!category.Equals(subcategory)) {
        path += "/" + subcategory.ToLower();
    }
    path += "/" + Regex.Replace(id, @"\s+", "") + ".jpg";
    return path;
  }

  public static Texture2D getTextureFromPic(string filePath){
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
}
