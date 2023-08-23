using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupEFX : EFX
{
    [HideInInspector] public double value;
    [SerializeField] private TMP_Text moneyText;


    void Start()
    {
        moneyText.text = SFNuffix.GetShortValue(value, 1);
    }
}
