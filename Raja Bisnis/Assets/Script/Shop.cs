using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Serve System")]
    [Range(0, 100)]
    public float serveCounter;

    [Space(10)]
    public ShopObject thisObject;
    public Transform[] targetPoint;
    public Transform[] doorPoint;
    public TMP_Text capacityText;

    public virtual void setShop()
    {
        RukoManager.instance.shopScenes.Add(this.gameObject.GetComponent<Shop>());

        updateCapacity();
    }

    public virtual void servingSystem()
    {
        if(thisObject.capacityShop > 0)
        {
            serveCounter += (float)thisObject.servingTimeShop * Time.deltaTime;
            if (serveCounter >= 100)
            {
                thisObject.capacityShop -= 1;
                thisObject.capacity_Temp -= 1;
                serveCounter = 0;

                GameManager.instance.GetMoney(thisObject.incomeShop, "popup");
                updateCapacity();
            }
        }

    }

    public virtual void updateCapacity()
    {
        capacityText.text = thisObject.capacityShop.ToString() + " / " + thisObject.capacityMax;

    }
}

