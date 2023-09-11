using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int versionId;
    public string versionName;

    [Space(10)]
    [TextAreaAttribute]
    public string datajson;

    [Space(10)]
    public List<Data_Shop> Shop = new List<Data_Shop>();

    public void Save()
    {
        //PlayerPrefs.SetInt("GameData_versionId", versionId);
        //PlayerPrefs.SetString("GameData_versionName", versionName);
        //PlayerPrefs.SetString("GameData_JSON", datajson);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/GameData.json", datajson);
    }

    public void Load()
    {
      //versionId =  PlayerPrefs.GetInt("GameData_versionId", 0);
      // versionName = PlayerPrefs.GetString("GameData_versionName", "Version 0.1");


        string filePath = Application.persistentDataPath + "/GameData.json";
        string versionPath = Application.persistentDataPath + "/version.json";

        if (System.IO.File.Exists(filePath))
        {
            // Baca isi file JSON
            datajson = System.IO.File.ReadAllText(filePath);

            versionId = int.Parse(System.IO.File.ReadAllText(versionPath));
        }
        else
        {
            Debug.Log("Data tidak ditemukan");
            versionId = 0;
            System.IO.File.WriteAllText(Application.persistentDataPath + "/version.json", versionId.ToString());

        }

    }

}

[System.Serializable]
public class Data_Shop{

    public string _name;
    public string _id;
    [TextAreaAttribute]
    public string _description;
    public Sprite _shop_sprite;

    [Space(10)]
    public List<Data_Menu> _menu = new List<Data_Menu>();
}



[System.Serializable]
public class Data_Menu
{
    public string _name;
    public string _id;

    [TextAreaAttribute]
    public string _description;
    public Sprite _menu_sprite;

    [Header("Calculate")]
    public int _level_Max;

    [Space(10)]
    public double _price;
    public double _price_multiplier;
    public double _price_unlock;

    [Space(10)]
    public double _income;
    public double _income_multiplier;

    [Space(10)]
    [HideInInspector]public string _id_Shop;
}