using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	XmlDocument xmlDoc;
    XmlNodeList items;
    public List <MapClass> data = new List <MapClass>();
    string[] mainCategory = new string[12] {
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
    string[][] selectedMaps = new string[26][] {
        new string []{"HK 1300", "HK 1303", "HK 1304", "HK 1305", "HK 1306", "HK 1308"},
        new string []{"HK 1123", "HK 0423", "HK 0907", "HK 0422a", "HK 0422b", "HK 0908", "HK 0909"},
        new string []{"HK 0883", "HK 0912", "HK 1641", "HK 0003", "HK 0914"},
        new string []{"HK 0488", "HK 0198", "HK 0258", "HK 0281", "HK 0188"},
        new string []{"HK 0322", "HK 0443", "HK 0469", "HK 0373", "HK 0136"},
        new string []{"HK 0020", "HK 0146", "HK 0327", "HK 0333", "HK 0355"},
        new string []{"HK 1647", "HK 1544", "HK 1581"},
        new string []{"HK 1564", "HK 0614", "HK 1647", "HK 1648", "HK 0204"},
        new string []{"HK 0385", "HK 0973", "HK 1111", "HK 0450", "HK 0516"},
        new string []{"HK 0839", "HK 1455", "HK 0138", "HK 0140", "HK 0838"},
        new string []{"HK 0160", "HK 0487", "HK 0521", "HK 1607"},
        new string []{"HK 1465", "HK 0379", "HK 0411", "HK 1025", "HK 0981"},
        new string []{"HK 0947", "HK 1106", "HK 1165", "HK 0005", "HK 0062"},
        new string []{"HK 0308", "HK 0746", "HK 1546", "HK 1574", "HK 1623"},
        new string []{"HK 0150", "HK 1643", "HK 0096", "HK 0560", "HK 0874"},
        new string []{"HK 1548 - 01", "HK 1593", "HK 0225", "HK 0137", "HK 0868"},
        new string []{"HK 0453", "HK 0884", "HK 1450", "HK 0662", "HK 0679"},
        new string []{"HK 0057", "HK 1098", "HK 0062", "HK 0196", "HK 0200"},
        new string []{"HK 0942", "HK 0632", "HK 1567", "HK 1572"},
        new string []{"HK 0001", "HK 0103", "HK 0104", "HK 0120", "HK 0247a"},
        new string []{"HK 0885", "HK 0134", "HK 0178", "HK 0179", "HK 0186"},
        new string []{"HK 1518", "HK 0209", "HK 0277", "HK 0731", "HK 1552"},
        new string []{"HK 1477", "HK 1481", "HK 1490", "HK 1496", "HK 1503"},
        new string []{"HK 1594", "HK 1604", "HK 1587", "HK 0312", "HK 1614"},
        new string []{"HK 0140", "HK 0168", "HK 0171", "HK 0172", "HK 0216"},
        new string []{"HK 0321", "HK 0325", "HK 0324", "HK 0326", "HK 0327"}
    };
    int iteration = 0;
	bool finishedLoading = false;

    int year;
    string language;
    string coordinate;
    string title;
    string source;
    int[] imageSize;
    string property;
    string description;
    string category;
    string subCategory;
    string location;
    string id;

    void Start ()
	{
		DontDestroyOnLoad(this); 
		LoadXML();
		StartCoroutine("AssignData"); 
	}

	void Update ()
	{
		if (finishedLoading)
		{
           //SceneManager.LoadScene("SampleScene");
           finishedLoading = false;
		}
	}

	void LoadXML()
	{
	    xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/res/maps_meta.xml");
        Debug.Log(xmlDoc.InnerXml);
        items = xmlDoc.SelectNodes("/collection/record");
	}

    bool idExistsInMap(string mapID)
    {
        for (int i = 0; i < selectedMaps.GetLength(0); i++)
        {
            for (int j = 0; j < selectedMaps[i].Length; j++)
            {
                if (selectedMaps[i][j] == mapID)
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator AssignData()
	{
        Debug.Log("Assignstart \n");
        Debug.Log(items.Count);
        foreach (XmlNode item in items)
        {
            var node = item.SelectSingleNode("datafield[@tag=\"852\"]/subfield[@code=\"m\"]");
            id = "KeineID";
            if (node != null)
                id = node.InnerText;

            if (idExistsInMap(id))
            {
                node = item.SelectSingleNode("datafield[@tag=\"841\"]/subfield[@code=\"b\"]");
                if (node != null)
                    year = int.Parse(node.InnerText);

                node = item.SelectSingleNode("datafield[@tag=\"041\"]/subfield[@code=\"a\"]");
                if (node != null)
                    language = node.InnerText;

                node = item.SelectSingleNode("datafield[@tag=\"225\"]/subfield[@code=\"c\"]");
                if (node != null)
                    coordinate = node.InnerText;
                
                node = item.SelectSingleNode("datafield[@tag=\"245\"]/subfield[@code=\"a\"]");
                if (node != null)
                    title = node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"245\"]/subfield[@code=\"b\"]");
                if (node != null)
                    title = title + "\n " + node.InnerText;
                
                node = item.SelectSingleNode("datafield[@tag=\"260\"]/subfield[@code=\"a\"]");
                if (node != null)
                    source = node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"260\"]/subfield[@code=\"b\"]");
                if (node != null)
                    source = source + "\n " + node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"260\"]/subfield[@code=\"c\"]");
                if (node != null)
                    source = source + "\n " + node.InnerText;

                node = item.SelectSingleNode("datafield[@tag=\"300\"]/subfield[@code=\"c\"]");
                if (node != null) {
                    string[] parts = node.InnerText.Split(' ');
                    imageSize[0] = int.Parse(parts[0]);
                    imageSize[1] = int.Parse(parts[2]);
                }

                node = item.SelectSingleNode("datafield[@tag=\"500\"]/subfield[@code=\"a\"]");
                if (node != null)
                    property = node.InnerText;

                node = item.SelectSingleNode("datafield[@tag=\"520\"]/subfield[@code=\"a\"]");
                if (node != null)
                    description = node.InnerText;

                category = "TODO";
                subCategory = "TODO";

                node = item.SelectSingleNode("datafield[@tag=\"651\"]/subfield[@code=\"a\"]");
                if (node != null)
                    location = node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"651\"]/subfield[@code=\"a\"][2]");
                if (node != null)
                    location = location + "\n " + node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"651\"]/subfield[@code=\"a\"][3]");
                if (node != null)
                    location = location + "\n " + node.InnerText;
                node = item.SelectSingleNode("datafield[@tag=\"651\"]/subfield[@code=\"a\"][4]");
                if (node != null)
                    location = location + "\n " + node.InnerText;


                data.Add(new MapClass(year, language, coordinate, title, source, imageSize, property, description, category, subCategory, location, id));
                Debug.Log("LOCATION: " + location);
            }

            iteration++;
        }

		finishedLoading = true; 
		return null;
	}
}