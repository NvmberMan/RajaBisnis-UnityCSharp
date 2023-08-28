using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Promo", order = 1)]
public class Promo : ScriptableObject
{
    public int currentLvl = 1;

    [Space(5)]
    [TextAreaAttribute]
    public string description = "Memanggil anak remaja 0.1 detik lebih cepat";
    public float addPercentage = 0.1f;
    public Sprite display;

    [Space(10)]
    public NpcItem[] npcCall;
    public double[] priceUpgrade;
}
