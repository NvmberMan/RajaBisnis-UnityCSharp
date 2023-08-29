using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContent : MonoBehaviour
{
    public static NavContent instance;
    private void Awake() { instance = this; }

    public Color highlightColor;

    public Transform menuContainer;
    public GameObject menuItemPrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateMenu()
    {
        ShopObject so = GameManager.instance.currentShopSelected;

        //refresh menu data
        foreach (Transform child in menuContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < so.menu.Count; i++)
        {
            //instiatiate all menu from databases
            MenuUI menu = Instantiate(menuItemPrefab, menuContainer.transform).GetComponent<MenuUI>();

            menu.thisShop = so;
            menu.indexMenu = i;

            double income = so.menu[i].income;
            double incomeMultiplier = so.menu[i].incomeMultiplier;


            double currentIncome = so.menu[i].incomeNow;
            double nextLevelIncome = income + incomeMultiplier * so.menu[i].currLevel + 1;


            string currentIncomeString = SFNuffix.GetShortValue(currentIncome, 1);
            string nextLevelIncomeString = SFNuffix.GetShortValue(nextLevelIncome, 1);

            //update menu data in page menu
            menu.nameText.text = so.menu[i].name;
            menu.levelText.text = "Lvl. " + so.menu[i].currLevel.ToString();
            menu.descriptionText.text = so.menu[i].description;




            menu.isLocked = so.menu[i].locked;
            menu.unlockPrice = so.menu[i].price;
            menu.display.sprite = so.menu[i].display;



            //check if level has max
            if (so.menu[i].currLevel >= so.menu[i].maxLevel)
            {
                menu.priceText.text = "Max";
                menu.incomeText.text = currentIncomeString;
            }
            else
            {
                menu.incomeText.text = currentIncomeString + "<color=#" + ColorUtility.ToHtmlStringRGB(highlightColor) +
                "> + " + nextLevelIncomeString + "</color>";


                double price = so.menu[i].price + ((so.menu[i].currLevel - 1) * so.menu[i].price + so.menu[i].priceMultiplier * (so.menu[i].currLevel - 1));
                menu.priceText.text = SFNuffix.GetShortValue(price);

            }

            //check if menu is locked
            if (so.menu[i].currLevel <= 0)
            {
                
            }

        }
    }

}
