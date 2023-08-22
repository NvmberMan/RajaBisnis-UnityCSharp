using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() { instance = this; }

    [Header("System Npc")]
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;

    [Space(10)]
    public Transform npcLeftPoint;
    public Transform npcRightPoint;
    public List<Npc> npcPrefabs = new List<Npc>();
}
