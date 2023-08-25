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
        ShopObject so = RukoManager.instance.currentShopSelected;
        foreach (Transform child in menuContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < so.menu.Count; i++)
        {
            MenuUI menu = Instantiate(menuItemPrefab, menuContainer.transform).GetComponent<MenuUI>();
            menu.nameText.text = so.menu[i].name;
            menu.descriptionText.text = so.menu[i].upgradeItem[so.menu[i].currLevel - 1].description;
            menu.incomeText.text = SFNuffix.GetShortValue(so.menu[i].upgradeItem[so.menu[i].currLevel - 1].income, 1) + " / Perorang";
            menu.priceText.text = SFNuffix.GetShortValue(so.menu[i].upgradeItem[so.menu[i].currLevel - 1].price, 1);

            menu.isLocked = so.menu[i].locked;
            menu.unlockPrice = so.menu[i].unlockPrice;
            menu.display.sprite = so.menu[i].display;

        }
    }
}
