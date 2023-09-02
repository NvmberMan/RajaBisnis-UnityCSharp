using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public EquipmentItem thisEquipment;
    public Image display;
    public TMP_Text levelText;

    public void clickThis()
    {
        EquipmentManager.instance.viewEquipment(thisEquipment);

        GameManager.instance.spawnSoundEfx(GameManager.instance.clickClip, 1, 0.5f);
    }
}
