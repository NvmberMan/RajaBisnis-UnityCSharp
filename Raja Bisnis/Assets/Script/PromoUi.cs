using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromoUi : MonoBehaviour
{
    public ShopObject thisShop;
    public int indexMenu;

    public TMP_Text titleText;
    public TMP_Text levelText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public Image display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgrade()
    {
        //check if level is not max
        if (thisShop.promo[indexMenu].currentLvl < thisShop.promo[indexMenu].promoObj.maxLevel)
        {
            double price = thisShop.promo[indexMenu].promoObj.price + ((thisShop.promo[indexMenu].currentLvl - 1) * thisShop.promo[indexMenu].promoObj.price + thisShop.promo[indexMenu].promoObj.priceMultiplier * (thisShop.promo[indexMenu].currentLvl - 1));
            //check player money
            if (GameManager.instance.money >= price)
            {
                thisShop.promo[indexMenu].spawnSpeedPercentage = thisShop.promo[indexMenu].promoObj.addPercentage * thisShop.promo[indexMenu].currentLvl;
                thisShop.promo[indexMenu].currentLvl += 1;

                PromoManager.instance.updatePromo();
                thisShop.updateData();
                //GameManager.instance.updateRukoManager();

                GameManager.instance.money -= price;
                GameManager.instance.UpdateMoney();
            }
            else
            {
                GameManager.instance.showAlert("Uang Kamu Tidak Cukup", 3);
            }

        }

        GameManager.instance.spawnSoundEfx(GameManager.instance.clickClip, 1, 0.5f);
    }
}
