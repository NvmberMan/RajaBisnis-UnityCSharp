using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Equipment thisEquipment;
    public Image display;
    public TMP_Text levelText;

    public void clickThis()
    {
        EquipmentManager.instance.viewEquipment(thisEquipment);
    }
}
