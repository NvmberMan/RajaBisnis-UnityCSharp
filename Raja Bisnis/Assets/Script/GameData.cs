using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/GameData", order = 1)]
public class GameData : ScriptableObject
{
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
    public string _menu_display;

    [Header("Calculate")]
    public int _level_Max;
    public double _price;
    public double _price_multiplier;
    public double _price_unlock;

    [Space(10)]
    public string _id_Shop;
}