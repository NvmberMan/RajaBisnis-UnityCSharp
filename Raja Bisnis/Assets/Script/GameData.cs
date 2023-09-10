using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int versionId;
    public string versionName;
    public List<Data_Shop> Shop = new List<Data_Shop>();


}

[System.Serializable]
public class Data_Shop{

    public string _name;
    public string _id;
    [TextAreaAttribute]
    public string _description;

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