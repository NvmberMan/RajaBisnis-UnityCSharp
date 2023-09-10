using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private Transform shopCardContainer;

    GameData gd;

    // Start is called before the first frame update
    void Start()
    {
        gd = GameManager.instance.GAMEDATA;

        UpdateBusiness();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBusiness()
    {
        foreach(Data_Shop shop in gd.Shop)
        {
            ShopCard sc = Instantiate(shopCardPrefab, shopCardContainer).GetComponent<ShopCard>();

        }
    }
}
