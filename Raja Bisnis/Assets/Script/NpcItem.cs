using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Npc/NormalNpc")]
public class NpcItem : ScriptableObject
{
    public float spawnMinTime = 4;
    public float spawnMaxTime = 7;
    public GameObject prefabs;

}
