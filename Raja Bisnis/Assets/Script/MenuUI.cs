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
        if(thisShop.menu[indexMenu].currLevel < thisShop.menu[indexMenu].maxLevel)
        {
            double price = thisShop.menu[indexMenu].price + ((thisShop.menu[indexMenu].currLevel - 1) * thisShop.menu[indexMenu].price + thisShop.menu[indexMenu].priceMultiplier * (thisShop.menu[indexMenu].currLevel - 1));
            //check player money
            if (GameManager.instance.money >= price)
            {

                double income = thisShop.menu[indexMenu].income;
                double incomeMultiplier = thisShop.menu[indexMenu].incomeMultiplier;
                double nextLevelIncome = income + incomeMultiplier * thisShop.menu[indexMenu].currLevel + 1;


                thisShop.menu[indexMenu].currLevel += 1;
                thisShop.menu[indexMenu].incomeNow += nextLevelIncome;

                NavContent.instance.updateMenu();
                thisShop.updateData();
                GameManager.instance.updateRukoManager();

                GameManager.instance.money -= price;
                GameManager.instance.UpdateMoney();
            }
            else
            {
                GameManager.instance.showAlert("Uang Kamu Tidak Cukup", 3);
            }
            GameManager.instance.spawnSoundEfx(GameManager.instance.clickClip, 1, 0.5f);
        }else
        {
            GameManager.instance.showAlert("Menu Ini Sudah Level Maximal", 2);
        }
    }

    public void unlock()
    {
        //check if level is locked
        if(thisShop.menu[indexMenu].currLevel == 0)
        {
            double price = thisShop.menu[indexMenu].price;
            //check player money
            if (GameManager.instance.money >= price)
            {
                thisShop.menu[indexMenu].currLevel = 1;

                NavContent.instance.updateMenu();
                thisShop.updateData();
                GameManager.instance.updateRukoManager();

                //update money
                GameManager.instance.money -= price;
                GameManager.instance.UpdateMoney();
            }
            else
            {
                GameManager.instance.showAlert("Uang Kamu Tidak Cukup", 3);
            }



        }
    }

}
