using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text incomeText;
    public TMP_Text priceText;
    public TMP_Text levelText;
    public TMP_Text unlockPriceText;
    public Button upgradeButton, unlockButton;
    public Image display;

    [HideInInspector] public bool isLocked;
    [HideInInspector] public double unlockPrice;
    [HideInInspector] public ShopObject thisShop;
    [HideInInspector] public int indexMenu;
    [SerializeField] private GameObject unlockMenu;
    [SerializeField] private TMP_Text lockMenu;


    private void Start()
    {
        if(thisShop)
        {
            if (thisShop.menu[indexMenu].currLevel <= 0)
            {
                unlockPriceText.text = SFNuffix.GetShortValue(unlockPrice);
                upgradeButton.gameObject.SetActive(false);
                unlockMenu.SetActive(true);

                unlockButton.gameObject.SetActive(true);

                if(thisShop.menu[indexMenu].levelShopRequire > thisShop.lvlShop)
                {
                    lockMenu.gameObject.SetActive(true);
                    lockMenu.text = "Perlu Toko Level " + thisShop.menu[indexMenu].levelShopRequire;
                    unlockButton.gameObject.SetActive(false);

                }
            }
        }

    }

    public void upgrade()
    {
        //check if level is not max
        if(thisShop.menu[indexMenu].currLevel < thisShop.menu[indexMenu].upgradeItem.Count)
        {
            thisShop.menu[indexMenu].currLevel += 1;

            NavContent.instance.updateMenu();
            thisShop.updateData();
            GameManager.instance.updateRukoManager();
        }
    }

    public void unlock()
    {
        //check if level is locked
        if(thisShop.menu[indexMenu].locked)
        {
            thisShop.menu[indexMenu].currLevel = 1;

            NavContent.instance.updateMenu();
            thisShop.updateData();
            GameManager.instance.updateRukoManager();


        }
    }

}
