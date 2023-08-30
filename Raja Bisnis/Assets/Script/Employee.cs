using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Employee", order = 1)]
public class Employee : ScriptableObject
{
    public string nameEmployee = "New Employee";
    public Sprite display;
    public int currentLvl = 1;

    [Range(0, 100)]
    public float serveSpeed = 5;
    [Range(0, 100)]
    public float restSpeed = 5;
    [Range(0, 100)]
    public float stamina = 5;
    [Range(0, 100)]
    public float attitude = 5;
}
