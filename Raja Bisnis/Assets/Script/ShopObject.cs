using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "GameData/Shop", order = 1)]
public class ShopObject : ScriptableObject
{
    public string nameShop = "Toko ...";
    public double buyPrice = 3000;
    public GameObject shopPrefab;
    public GameObject cardPrefab;
    public Sprite displayShop;

    [Header("Level")]
    public int lvlShop = 1;
    public int lvl_Default = 1;

    [Header("Income")]
    public double incomeShop = 20;
    public double incomeShop_Default = 20;

    [Header("Bank")]
    public double bankShop = 0;
    public double bank_Default = 0;
    public double bankMax = 100;

    [Header("Tips")]
    public float tipsShop = 0;
    public float tipsDefault = 0;

    [Space(8)]
    public float tipsChange = 100;
    public float tipsChangeDefault = 100;

    [Header("Serving")]
    public double servingTimeShop = 4;

    [Space(25)]
    [Header("-------------------------------------------------------------------")]

    [Header("Capacity")]
    public List<NpcItem> capacityNPC = new List<NpcItem>();

    [Header("Experience")]
    public List<Experience> expShop = new List<Experience>();

    [Header("Menus")]
    public List<Menu> menu = new List<Menu>();

    //[Space(10)]
    //[Header("Employee")]
    //public List<Employee> employee = new List<Employee>();

    [Header("Promo")]
    public List<PromoItem> promo = new List<PromoItem>();

    [Header("Equipment")]
    public List<EquipmentItem> equipment = new List<EquipmentItem>();

    [HideInInspector]public bool hasNewGame = false;

    public void updateData()
    {

        incomeShop = 0;
        for (int i = 0; i < menu.Count; i++)
        {
            if (menu[i].currLevel >= 1)
                incomeShop += menu[i].incomeNow;
        }
    }

    public void newShop()
    {
        if (!hasNewGame)
        {
            lvlShop = lvl_Default;

            incomeShop = incomeShop_Default;

            tipsShop = tipsDefault;
            tipsChange = tipsChangeDefault;



            //reset Exp
            for (int i = 0; i < expShop.Count; i++)
            {
                expShop[i].exp = 0;
            }

            //reset Menu level
            for (int i = 0; i < menu.Count; i++)
            {
                menu[i].currLevel = 1;
                menu[i].incomeNow = menu[i].incomeDefault;

                if (menu[i].locked)
                {
                    menu[i].currLevel = 0;
                }
            }

            //reset Equipment
            for (int i = 0; i < equipment.Count; i++)
            {
                equipment[i].currentLvl = 1;
            }

            //reset Promo
            for (int i = 0; i < promo.Count; i++)
            {
                promo[i].currentLvl = 1;
                promo[i].spawnSpeedPercentage = 0;
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
    public int currLevel = 1;
    public int maxLevel = 20;
    [Space(10)]
    public string name = "New Menu";

    [TextAreaAttribute]
    public string description = "Ayam geprek adalah makanan khas bogor yang sangat populer dikalangan remaja";
    public Sprite display;

    [Space(10)]
    public bool locked;
    public int levelShopRequire = 0;


    [Space(10)]
    [HideInInspector]public double incomeNow = 2;
    public double incomeDefault = 2;

    [Header("Income Upgraded")]
    public double income = 2;
    public double incomeMultiplier = 1;

    [Space(10)]
    public double price = 10;
    public double priceMultiplier = 1;
}

[System.Serializable]
public class PromoItem
{
    public Promo promoObj;
    public int currentLvl = 1;
    [Space(5)]
    public float spawnSpeedPercentage;
}


[System.Serializable]
public class EquipmentItem
{
    public Equipment equipmentObj;
    public int currentLvl = 1;

}



