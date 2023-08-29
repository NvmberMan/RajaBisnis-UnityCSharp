using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Equipment", order = 1)]
public class Equipment : ScriptableObject
{

    public string nameEquipment = "New Equipment";
    public string description = "Description of equipment";
    public Sprite display;
    public int maxLevel = 50;

    [Space(10)]
    public float tip = 0.1f;
    public float tipChange = 1;
    [Space(10)]
    public double price = 10;
    public double priceMultiplier = 1;

}

