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
    [SerializeField] private TMP_Text advantageText;
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

    public void updateEquipment()
    {

        currentShopSelected = GameManager.instance.currentShopSelected;

        //set default view Employee
        if (currentShopSelected.equipment.Count > 0)
        {
            viewEquipment(currentShopSelected.equipment[0]);
        }

        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        //Clone employee in container
        for (int i = 0; i < currentShopSelected.equipment.Count; i++)
        {
            EquipmentUI em = Instantiate(equipmentUIPrefab, gridContainer).GetComponent<EquipmentUI>();

            em.thisEquipment = currentShopSelected.equipment[i];

            if (currentShopSelected.equipment[i].display != null)
                em.display.sprite = currentShopSelected.equipment[i].display;

            em.levelText.text = currentShopSelected.equipment[i].currentLvl.ToString();

        }
    }

    public void viewEquipment(Equipment eq)
    {
        if (eq.display != null)
            display.sprite = eq.display;

        nameText.text = eq.name + " Lvl. " + eq.currentLvl.ToString();
        advantageText.text = "+" + eq.upgrade[eq.currentLvl - 1].capacityAdd.ToString() + " Kapasitas";
    }

}
