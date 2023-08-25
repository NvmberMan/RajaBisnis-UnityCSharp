using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RukoManager : MonoBehaviour
{
    public static RukoManager instance;
    private void Awake() { instance = this; }

    public ShopObject currentShopSelected;
    public bool newGame;

    [Header("Ruko Manager")]
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text capacityText;
    [SerializeField] private TMP_Text servingTimeText;
    [SerializeField] private TMP_Text incomeText;

    [Space(10)]
    [SerializeField] private Image display;
    [SerializeField] private Slider experienceSlider;

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
            for (int i = 0; i < shopObjects.Length; i++)
            {
                shopObjects[i].newShop();
                shopObjects[i].hasNewGame = true;

            }
        }

        //update ui di atas ruko
        for (int i = 0; i < shopScenes.Count; i++)
        {
            shopScenes[i].updateCapacity();
        }
    }

    void Update()
    {
        
    }

    public void updateRukoManager()
    {
        if(currentShopSelected != null)
        {

            display.sprite = currentShopSelected.displayShop;
            lvlText.text = "Lvl." + currentShopSelected.lvlShop.ToString();
            capacityText.text = currentShopSelected.capacityShop.ToString() + " / " + currentShopSelected.capacityMax.ToString();
            servingTimeText.text = currentShopSelected.servingTimeShop.ToString() + " / menit";
            incomeText.text = currentShopSelected.incomeShop.ToString() + " / Hari";
            experienceSlider.maxValue = currentShopSelected.expShop[currentShopSelected.lvlShop - 1].max;
            experienceSlider.value = currentShopSelected.expShop[currentShopSelected.lvlShop - 1].exp;
        }

    }
}
