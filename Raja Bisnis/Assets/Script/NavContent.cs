using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContent : MonoBehaviour
{
    public static NavContent instance;
    private void Awake() { instance = this; }

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
            int decrement = 1;

            //check if menu is locked
            if (so.menu[i].currLevel <= 0)
            {
                decrement = 0;
            }

            menu.thisShop = so;
            menu.indexMenu = i;

            //update menu data in page menu
            menu.nameText.text = so.menu[i].name;
            menu.levelText.text = "Lvl. " + so.menu[i].currLevel.ToString();
            menu.descriptionText.text = so.menu[i].upgradeItem[so.menu[i].currLevel - decrement].description;
            menu.incomeText.text = SFNuffix.GetShortValue(so.menu[i].upgradeItem[so.menu[i].currLevel - decrement].income, 1) + " / Perorang";
            menu.priceText.text = SFNuffix.GetShortValue(so.menu[i].upgradeItem[so.menu[i].currLevel - decrement].price, 1);

            menu.isLocked = so.menu[i].locked;
            menu.unlockPrice = so.menu[i].unlockPrice;
            menu.display.sprite = so.menu[i].display;



            //check if level has max
            if (so.menu[i].currLevel >= so.menu[i].upgradeItem.Count)
            {
                menu.priceText.text = "Max";
            }

        }
    }

}