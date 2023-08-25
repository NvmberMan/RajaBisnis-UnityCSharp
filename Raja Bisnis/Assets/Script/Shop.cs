using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public ShopObject thisObject;
    public Transform[] targetPoint;
    public Transform[] doorPoint;
    public TMP_Text capacityText;

    public virtual void setShop()
    {
        RukoManager.instance.shopScenes.Add(this.gameObject.GetComponent<Shop>());

        updateCapacity();
    }

    public virtual void updateCapacity()
    {
        capacityText.text = thisObject.capacityShop.ToString() + " / " + thisObject.capacityMax;

    }
}

