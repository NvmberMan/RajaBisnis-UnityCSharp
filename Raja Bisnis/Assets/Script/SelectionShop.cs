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

        CanvasManager.instance.GetComponent<Animator>().Play("NavScroll_Open_From_Choose");
        GameManager.instance.canvasGame.GetComponent<Animator>().Play("CanvasGame_Up_High");
    }
}
