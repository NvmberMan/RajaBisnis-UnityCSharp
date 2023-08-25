using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/Employee", order = 1)]
public class Employee : ScriptableObject
{
    public string nameEmployee = "New Employee";
    [Range(0, 100)]
    public float serveSpeed = 5;
}
