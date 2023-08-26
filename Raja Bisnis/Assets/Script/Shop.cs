using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Serve System")]
    [Range(0, 5)]
    public float serveCounter;

    [Space(10)]
    public ShopObject thisObject;
    public Transform[] targetPoint;
    public Transform[] doorPoint;
    public TMP_Text capacityText;

    public virtual void setShop()
    {
        //add shop gameobject to rukomanager
        RukoManager.instance.shopScenes.Add(this.gameObject.GetComponent<Shop>());

    }

    public virtual void servingSystem()
    {
        //check if shop is has a customer
        if(thisObject.capacityNPC.Count > 0)
        {
            serveCounter += (float)thisObject.servingTimeShop * Time.deltaTime;
            if (serveCounter >= 5)
            {
                serveCounter = 0;
                GameManager.instance.GetMoney(thisObject.incomeShop, "popup");

                //thisObject.capacityShop -= 1;
                //npc exit from shop
                NpcItem ni = thisObject.capacityNPC[0];
                Npc npc = Instantiate(ni.prefabs, doorPoint[Random.Range(0, doorPoint.Length)].position, Quaternion.identity, GameManager.instance.npcLeftPoint).GetComponent<Npc>();
                npc.roadTarget = targetPoint[Random.Range(0, targetPoint.Length)];

                int randomDirection = Random.Range(0, 100);

                if (randomDirection > 50)
                {
                    npc.walkingDirection = Vector2.right;
                    npc.transform.localScale = new Vector2(-npc.transform.localScale.x, npc.transform.localScale.y);
                    npc.transform.SetParent(GameManager.instance.npcLeftPoint);
                }
                else
                {
                    npc.walkingDirection = Vector2.left;
                    npc.transform.SetParent(GameManager.instance.npcRightPoint);

                }

                thisObject.capacityNPC.RemoveAt(0);
            }
        }

    }
}

