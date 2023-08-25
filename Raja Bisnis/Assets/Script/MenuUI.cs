using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text incomeText;
    public TMP_Text priceText;
    public TMP_Text unlockPriceText;
    public Image display;

    [HideInInspector] public bool isLocked;
    [HideInInspector] public double unlockPrice;
    [SerializeField] private GameObject unlockMenu;


    private void Start()
    {
        if(isLocked)
        {
            unlockPriceText.text = SFNuffix.GetShortValue(unlockPrice);
            unlockMenu.SetActive(true);
        }
    }

}
