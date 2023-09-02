using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake() { instance = this; }

    [Header("Game Data")]
    public EquipmentItem selectedEquipment;
    
    [Header("Scene Component")]
    [SerializeField] private Image display;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text tipsText, tipsChangeText;
    [SerializeField] private TMP_Text priceText;

    [Space(5)]
    [SerializeField] private GameObject equipmentUIPrefab;
    [SerializeField] private Transform gridContainer;

    private ShopObject currentShopSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateEquipment(bool defaultSelect = true)
    {

        currentShopSelected = GameManager.instance.currentShopSelected;

        //set default view Equipment
        if (currentShopSelected.equipment.Count > 0 && defaultSelect)
        {
            viewEquipment(currentShopSelected.equipment[0]);
        }

        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        //Clone Equipment in container
        for (int i = 0; i < currentShopSelected.equipment.Count; i++)
        {
            EquipmentUI em = Instantiate(equipmentUIPrefab, gridContainer).GetComponent<EquipmentUI>();

            em.thisEquipment = currentShopSelected.equipment[i];

            if (currentShopSelected.equipment[i].equipmentObj.display != null)
                em.display.sprite = currentShopSelected.equipment[i].equipmentObj.display;

            em.levelText.text = currentShopSelected.equipment[i].currentLvl.ToString();

        }
    }

    //view selected equipment
    public void viewEquipment(EquipmentItem eq)
    {
        selectedEquipment = eq;

        if (eq.equipmentObj.display != null)
            display.sprite = eq.equipmentObj.display;

        nameText.text = eq.equipmentObj.name + " Lvl. " + eq.currentLvl.ToString();
        descriptionText.text = eq.equipmentObj.description;



        //check if max equipment level
        if (eq.currentLvl >= selectedEquipment.equipmentObj.maxLevel)
        {
            tipsText.text = (eq.equipmentObj.tip * eq.currentLvl - 1) + "% Income";
            tipsChangeText.text = (eq.equipmentObj.tipChange * eq.currentLvl - 1) + "% Tips Change";
            priceText.text = "Max";
        }else
        {
            tipsText.text = "+" + (eq.equipmentObj.tip * eq.currentLvl) + "% Income";
            tipsChangeText.text = "+" + (eq.equipmentObj.tipChange * eq.currentLvl) + "% Tips Change";

            double price = eq.equipmentObj.price + ((eq.currentLvl - 1) * eq.equipmentObj.price + eq.equipmentObj.priceMultiplier * (eq.currentLvl - 1));
            priceText.text = SFNuffix.GetShortValue(price);
        }
        tipsChangeText.gameObject.SetActive(true);

        if (eq.equipmentObj.tipChange <= 0)
        {
            tipsChangeText.gameObject.SetActive(false);
        }
    }

    public void upgradeEquipment()
    {
        //check if max equipment level
        if(selectedEquipment.currentLvl < selectedEquipment.equipmentObj.maxLevel)
        {
            EquipmentItem eq = selectedEquipment;

            double price = eq.equipmentObj.price + ((eq.currentLvl - 1) * eq.equipmentObj.price + eq.equipmentObj.priceMultiplier * (eq.currentLvl - 1));
            //check our money
            if (price <= GameManager.instance.money)
            {
                //start money animation
                //

                GameManager.instance.money -= price;
                GameManager.instance.UpdateMoney();


                currentShopSelected.tipsShop += selectedEquipment.equipmentObj.tip;
                currentShopSelected.tipsChange += selectedEquipment.equipmentObj.tipChange;

                selectedEquipment.currentLvl += 1;

                updateEquipment(false);
                viewEquipment(selectedEquipment);
                GameManager.instance.updateEquipmentManager();
            }else
            {
                GameManager.instance.showAlert("Uang Kamu Tidak Cukup", 3);

            }
        }



    }

}
