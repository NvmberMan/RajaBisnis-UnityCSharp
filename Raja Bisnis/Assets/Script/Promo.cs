using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Promo", order = 1)]
public class Promo : ScriptableObject
{
    [TextAreaAttribute]
    public string description = "Memanggil anak remaja";
    [TextAreaAttribute]
    public string description2 = "detik lebih cepat";
    [Space(5)]
    public float addPercentage = 0.1f;
    public Sprite display;

    [Space(10)]
    public NpcItem[] npcCall;
    [Space(10)]
    public int maxLevel = 20;
    public double price = 10;
    public double priceMultiplier = 1;
}
