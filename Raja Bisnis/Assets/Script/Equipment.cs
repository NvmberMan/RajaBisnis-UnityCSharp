using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Equipment", order = 1)]
public class Equipment : ScriptableObject
{

    public string nameEquipment = "New Equipment";
    public string description = "Description of equipment";
    public Sprite display;
    public int currentLvl = 1;

    [Space(10)]
    public List<EquipmentUpgrade> upgrade = new List<EquipmentUpgrade>() { new EquipmentUpgrade() };

}

[System.Serializable]
public class EquipmentUpgrade
{
    public float tip = 1;
    public float tipChange = 0.1f;
    [Space(10)]
    public double price = 100;
}
