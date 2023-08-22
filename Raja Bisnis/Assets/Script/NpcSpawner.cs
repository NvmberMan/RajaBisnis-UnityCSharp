using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        StartCoroutine(spawnNpc());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnNpc()
    {
        yield return new WaitForSeconds(Random.Range(gm.minSpawnTime, gm.maxSpawnTime));
        int randomDirection = Random.Range(0, 100);


        Transform npcContainer = gm.npcLeftPoint.parent;
        Transform npcSpawnPoint = randomDirection > 50 ? gm.npcLeftPoint : gm.npcRightPoint;

        Npc npc = Instantiate(gm.npcPrefabs[Random.Range(0,gm.npcPrefabs.Count)], npcSpawnPoint.position, Quaternion.identity, npcContainer);

        if(randomDirection > 50)
        {
            npc.walkingDirection = Vector2.right;
        }else
        {
            npc.walkingDirection = Vector2.left;
        }

        StartCoroutine(spawnNpc());

    }
}
