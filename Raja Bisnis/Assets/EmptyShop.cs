using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmptyShop : MonoBehaviour
{
    public double price = 5000;
     public GameObject lockedMenu, setShopButton;
    [SerializeField] private TMP_Text priceText;

    private void Start()
    {
        updatePrice();
    }

    public void updatePrice()
    {
        int count = 0;

        for(int i = 0; i < RukoManager.instance.shopEmpty.Count; i++)
        {
            if(RukoManager.instance.shopEmpty[i].shop)
            {
                EmptyShop es = RukoManager.instance.shopEmpty[i].shop;


                if (!es.lockedMenu.activeSelf)
                {
                    count++;

                }
            }

        }

        price = CanvasManager.instance.priceShop[count];

        if(price == 0)
        {
            priceText.text = "Free";
        }
        else
            priceText.text = SFNuffix.GetShortValue(price, 1);
    }

    public void locked()
    {
        CanvasManager.instance.selectedEmpty = this;
        CanvasManager.instance.openLockedMenu(this);
    }

    public void unlock()
    {
        lockedMenu.SetActive(false);
        setShopButton.SetActive(true);

        Animator anim = GetComponent<Animator>();

        anim.Play("UnlockEmptyShop");
    }

    public void setShop()
    {
        CanvasManager.instance.selectedEmpty = this;
        CanvasManager.instance.openSetShop();
    }
}
