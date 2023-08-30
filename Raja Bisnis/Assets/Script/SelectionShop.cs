using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionShop : MonoBehaviour
{
    public ShopObject shopObject;
    public Image display;


    private void Start()
    {
        display.sprite = shopObject.displayShop;
    }

    public void selectShop()
    {
        GameManager.instance.currentShopSelected = shopObject;
        GameManager.instance.updateRukoManager();
    }
}
