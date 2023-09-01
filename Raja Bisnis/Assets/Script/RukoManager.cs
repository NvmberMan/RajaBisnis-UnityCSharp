using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RukoManager : MonoBehaviour
{
    public static RukoManager instance;
    private void Awake() { instance = this; }

    public bool newGame;


    public int shopBuy = 0;
    [Space(10)]
    public ShopObject[] shopObjects;
    public List<ShopScene> shopScenes = new List<ShopScene>();
    public List<ShopEmpty> shopEmpty = new List<ShopEmpty>();


    void Start()
    {
        for (int i = 0; i < shopObjects.Length; i++)
        {
            shopObjects[i].hasNewGame = false;
        }

        updateEmptyShop();
 

        if (newGame)
        {
            //reload all data to default
            for (int i = 0; i < shopObjects.Length; i++)
            {
                shopObjects[i].newShop();
                shopObjects[i].updateData();

                shopObjects[i].hasNewGame = true;

            }
        }


    }

    public void updateEmptyShop()
    {
        shopEmpty.Clear();

        foreach (Transform child in CanvasManager.instance.shopContainer.transform)
        {
            if (child.GetComponent<EmptyShop>())
            {

                    ShopEmpty ss = new ShopEmpty();
                    ss.shop = child.GetComponent<EmptyShop>();
                    ss.indexOrder = child.GetSiblingIndex();

                    shopEmpty.Add(ss);
                

            }

        }

    }

    void Update()
    {
 
    }

}

[System.Serializable]
public class ShopScene
{
    public Shop shop;
    public int indexOrder;
}


[System.Serializable]
public class ShopEmpty
{
    public EmptyShop shop;
    public int indexOrder;
}