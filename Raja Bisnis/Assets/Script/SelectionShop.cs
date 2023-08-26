using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionShop : MonoBehaviour
{
    public ShopObject shopObject;


    public void selectShop()
    {
        GameManager.instance.currentShopSelected = shopObject;
        GameManager.instance.updateRukoManager();
    }
}
