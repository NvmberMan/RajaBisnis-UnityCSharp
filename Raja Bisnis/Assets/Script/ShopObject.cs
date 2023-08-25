using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "GameData/Shop", order = 1)]
public class ShopObject : ScriptableObject
{
    public string nameShop = "Toko ...";
    public Sprite displayShop;

    [Header("Level")]
    public int lvlShop = 1;
    public int lvl_Default = 1;
    public int lvlMax = 5;

    [Header("Income")]
    public double incomeShop = 20;
    public double incomeShop_Default = 20;

    [Header("Capacity")]
    public int capacityShop = 0;
    public int capacity_Default = 0;
    public int capacityMax = 100;
    public int capacity_Temp = 0;

    [Header("Bank")]
    public double bankShop = 0;
    public double bank_Default = 0;
    public double bankMax = 100;

    [Header("Serving")]
    public double servingTimeShop = 4;
    public double servingTimeShop_Default = 4;

    [Header("Experience")]
    public List<Experience> expShop = new List<Experience>();

    [Space(10)]
    [Header("Menus")]
    public List<Menu> menu = new List<Menu>();

    [Space(10)]
    [Header("Employee")]
    public List<Employee> employee = new List<Employee>();

    [HideInInspector]public bool hasNewGame = false;

    public void newShop()
    {
        if(!hasNewGame)
        {
            lvlShop = lvl_Default;
            incomeShop = incomeShop_Default;
            capacityShop = capacity_Default;
            servingTimeShop = servingTimeShop_Default;

            capacity_Temp = 0;

            for(int i = 0; i < expShop.Count; i++)
            {
                expShop[i].exp = 0;
            }
        }

    }
}


[System.Serializable]
public class Experience
{
    public int exp;
    public int max;
}


[System.Serializable]
public class Menu
{
    public string name;
    public int currLevel;
    public Sprite display;

    [Space(10)]
    public bool locked;
    public double unlockPrice;

    public List<UpgradeItem> upgradeItem = new List<UpgradeItem>();
}


[System.Serializable]
public class UpgradeItem
{
    [TextAreaAttribute]
    public string description;
    public double income;
    public double price;

}

