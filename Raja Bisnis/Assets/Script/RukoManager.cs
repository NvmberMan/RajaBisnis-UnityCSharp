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



    [Space(10)]
    public ShopObject[] shopObjects;
    public List<Shop> shopScenes = new List<Shop>();


    void Start()
    {
        for (int i = 0; i < shopObjects.Length; i++)
        {
            shopObjects[i].hasNewGame = false;
        }

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

    void Update()
    {
 
    }

}
