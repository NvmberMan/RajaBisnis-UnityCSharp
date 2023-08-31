using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    private void Awake() { instance = this; }

    public double[] priceShop = { 0 };

    public GameObject shopContainer;
    public GameObject selectionContainer;
    public GameObject selectionPrefab;
    public GameObject lockMenu;
    public TMP_Text priceShopText;

    [HideInInspector]public EmptyShop selectedEmpty;


    //Unlock Empty Shop
    public void openLockedMenu(EmptyShop es)
    {
        Animator anim = lockMenu.GetComponent<Animator>();

        if (es.price == 0)
        {
            priceShopText.text = "Free";
        }
        else
            priceShopText.text = SFNuffix.GetShortValue(es.price, 1);

        anim.Play("OpenSelectionRent");

        selectedEmpty = es;
    }

    public void closeLockedMenu()
    {
        Animator anim = lockMenu.GetComponent<Animator>();

        anim.Play("CloseSelectionRent");

    }

    public void unlockEmptyShop()
    {
        if(selectedEmpty)
            selectedEmpty.unlock();

        closeLockedMenu();
        Invoke("openSetShop", 0.25f);

        foreach(Transform child in shopContainer.transform)
        {
            if(child.gameObject.GetComponent<EmptyShop>())
            {
                child.gameObject.GetComponent<EmptyShop>().updatePrice();
            }
        }

        RukoManager.instance.updateEmptyShop();
    }

    //Set Shop To Empty Shop
    public void openSetShop()
    {
        NavContent.instance.updateCard();

        Animator anim = lockMenu.GetComponent<Animator>();
        anim.Play("OpenSelectingShop");


    }

    public void closeSetShop()
    {
        Animator anim = lockMenu.GetComponent<Animator>();
        anim.Play("CloseSelectingShop");
    }

    public void pilihShop(ShopObject so)
    {
        GameObject shop = Instantiate(so.shopPrefab, shopContainer.transform);
        shop.transform.SetSiblingIndex(selectedEmpty.transform.GetSiblingIndex());

        Destroy(selectedEmpty.gameObject);

        SelectionShop selection = Instantiate(selectionPrefab, selectionContainer.transform).GetComponent<SelectionShop>();
        selection.shopObject = so;

        closeSetShop();

    }
}
