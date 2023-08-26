using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Equipment", order = 1)]
public class Equipment : ScriptableObject
{

    public string nameEquipment = "New Equipment";
    public Sprite display;
    public int currentLvl = 1;

    [Space(10)]
    public List<EquipmentUpgrade> upgrade = new List<EquipmentUpgrade>() { new EquipmentUpgrade() };

}

[System.Serializable]
public class EquipmentUpgrade
{
    public int capacityAdd = 1;
    public float bankAdd = 0;
    public float comfyAdd = 0;
}
