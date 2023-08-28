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
    public Equipment selectedEquipment;
    
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

            if (currentShopSelected.equipment[i].display != null)
                em.display.sprite = currentShopSelected.equipment[i].display;

            em.levelText.text = currentShopSelected.equipment[i].currentLvl.ToString();

        }
    }

    //view selected equipment
    public void viewEquipment(Equipment eq)
    {
        selectedEquipment = eq;

        if (eq.display != null)
            display.sprite = eq.display;

        nameText.text = eq.name + " Lvl. " + eq.currentLvl.ToString();
        descriptionText.text = eq.description;
        tipsText.text = "+" + eq.upgrade[eq.currentLvl - 1].tip.ToString() + "% Income";
        tipsChangeText.text = "+" + eq.upgrade[eq.currentLvl - 1].tipChange.ToString() + "% Tips Change";


        //check if max equipment level
        if (selectedEquipment.currentLvl >= selectedEquipment.upgrade.Count)
        {
            priceText.text = "Max";
        }else
        {
            priceText.text = SFNuffix.GetShortValue(eq.upgrade[eq.currentLvl].price);
        }
    }

    public void upgradeEquipment()
    {
        //check if max equipment level
        if(selectedEquipment.currentLvl < selectedEquipment.upgrade.Count)
        {
            //check our money
            if (selectedEquipment.upgrade[selectedEquipment.currentLvl].price <= GameManager.instance.money)
            {
                //start money animation
                //

                GameManager.instance.money -= selectedEquipment.upgrade[selectedEquipment.currentLvl].price;
                GameManager.instance.UpdateMoney();

                selectedEquipment.currentLvl += 1;

                updateEquipment(false);
                viewEquipment(selectedEquipment);
            }else
            {
                GameManager.instance.showAlert("Uang Kamu Tidak Cukup", 3);

            }
        }



    }

}
