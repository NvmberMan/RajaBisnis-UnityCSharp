using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoManager : MonoBehaviour
{
    public static PromoManager instance;
    private void Awake() { instance = this; }

    [SerializeField] private GameObject promoPrefab;
    [SerializeField] private Transform promoContainer;


    public void updatePromo()
    {
        ShopObject so = GameManager.instance.currentShopSelected;

        //refresh menu data
        foreach (Transform child in promoContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < so.promo.Count; i++)
        {
            //instiatiate all menu from databases
            PromoUi promo = Instantiate(promoPrefab, promoContainer.transform).GetComponent<PromoUi>();

            float spawnSpeed = so.promo[i].currentLvl * so.promo[i].promoObj.addPercentage;

            promo.thisShop = so;
            promo.indexMenu = i;

            //update menu data in page menu
            promo.titleText.text = so.promo[i].promoObj.name;
            promo.levelText.text = "Lvl. " + so.promo[i].currentLvl.ToString();
            promo.descriptionText.text = so.promo[i].promoObj.description + " " + spawnSpeed + " " + so.promo[i].promoObj.description2;
            promo.display.sprite = so.promo[i].promoObj.display;


            double price = so.promo[i].promoObj.price + ((so.promo[i].currentLvl - 1) * so.promo[i].promoObj.price + so.promo[i].promoObj.priceMultiplier * (so.promo[i].currentLvl - 1));
            //check if level has max
            if (so.promo[i].currentLvl >= so.promo[i].promoObj.maxLevel)
            {
                promo.priceText.text = "Max";
            }
            else
            {
                promo.priceText.text = SFNuffix.GetShortValue(price);
            }


        }
    }
}
