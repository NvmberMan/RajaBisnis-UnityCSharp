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


    [Header("Bank")]
    public double bankShop = 0;
    public double bank_Default = 0;
    public double bankMax = 100;

    [Header("Serving")]
    public double servingTimeShop = 4;
    public double servingTimeShop_Default = 4;

    [Header("Capacity")]
    public List<NpcItem> capacityNPC = new List<NpcItem>();

    [Header("Experience")]
    public List<Experience> expShop = new List<Experience>();

    [Space(10)]
    [Header("Menus")]
    public List<Menu> menu = new List<Menu>();

    //[Space(10)]
    //[Header("Employee")]
    //public List<Employee> employee = new List<Employee>();

    [Space(10)]
    [Header("Equipment")]
    public List<Equipment> equipment = new List<Equipment>();

    [HideInInspector]public bool hasNewGame = false;

    public void updateData()
    {

        incomeShop = 0;
        for (int i = 0; i < menu.Count; i++)
        {
            if(menu[i].currLevel >= 1)
                incomeShop += menu[i].upgradeItem[menu[i].currLevel - 1].income;
        }
    }

    public void newShop()
    {
        if (!hasNewGame)
        {
            lvlShop = lvl_Default;
            incomeShop = incomeShop_Default;
            servingTimeShop = servingTimeShop_Default;



            //reset Exp
            for (int i = 0; i < expShop.Count; i++)
            {
                expShop[i].exp = 0;
            }

            //reset Menu level
            for (int i = 0; i < menu.Count; i++)
            {
                menu[i].currLevel = 1;

                if (menu[i].locked)
                {
                    menu[i].currLevel = 0;
                }
            }

            //reset Npc
            capacityNPC.Clear();

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
    public string name = "New Menu";
    public int currLevel = 1;
    public Sprite display;

    [Space(10)]
    public bool locked;
    public double unlockPrice;
    public int levelShopRequire = 0;

    public List<UpgradeItem> upgradeItem = new List<UpgradeItem>();
}


[System.Serializable]
public class UpgradeItem
{
    [TextAreaAttribute]
    public string description = "Ayam geprek adalah makanan khas bogor yang sangat populer dikalangan remaja";
    public double income = 1000;
    public double price = 2000;

}

