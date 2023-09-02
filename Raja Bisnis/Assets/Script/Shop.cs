using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Serve System")]
    [Range(0, 5)]

    [Space(10)]
    public ShopObject thisObject;
    public Transform[] targetPoint;
    public Transform uiTransform;

    GameManager gm;

    public virtual void setShop()
    {
        //add shop gameobject to rukomanager
        ShopScene ss = new ShopScene();
        ss.shop = this.gameObject.GetComponent<Shop>();
        ss.indexOrder = transform.GetSiblingIndex();

        RukoManager.instance.shopScenes.Add(ss);


        gm = GameManager.instance;


        //start call npc
        for(int i = 0; i < thisObject.promo.Count; i++)
        {
            StartCoroutine(spawnNpc(i));
        }


    }

    public virtual void servingSystem()
    {
        //check if shop is has a customer
        if(thisObject.capacityNPC.Count > 0)
        {
            for (int i = 0; i < thisObject.capacityNPC.Count; i++)
            {
                thisObject.capacityNPC[i].counter += (float)thisObject.servingTimeShop * Time.deltaTime;
                if (thisObject.capacityNPC[i].counter >= 5)
                {
                    thisObject.capacityNPC[i].counter = 0;

                    //thisObject.capacityShop -= 1;

                    //npc exit from shop
                    NpcItem ni = thisObject.capacityNPC[0].npc;
                    Npc npc = Instantiate(ni.prefabs, targetPoint[Random.Range(0, targetPoint.Length)].position, Quaternion.identity, GameManager.instance.npcLeftPoint).GetComponent<Npc>();
                    npc.roadTarget = targetPoint[Random.Range(0, targetPoint.Length)];
                    npc.thisNpc = ni;

                    if (thisObject.lvlShop <= thisObject.expShop.Count)
                    {
                        thisObject.expShop[thisObject.lvlShop - 1].exp += npc.thisNpc.experienceCounter;
                        int exp = thisObject.expShop[thisObject.lvlShop - 1].exp;
                        int max = thisObject.expShop[thisObject.lvlShop - 1].max;
                        int sisa = 0;

                        if (exp >= max)
                        {
                            sisa = exp - max;
                            thisObject.lvlShop += 1;
                            thisObject.expShop[thisObject.lvlShop - 1].exp += sisa;

                            NavContent.instance.updateMenu();
                        }
                    }
                    if(GameManager.instance.currentShopSelected)
                    {
                        GameManager.instance.updateExpererience();

                    }

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

                    //tips system

                    if (thisObject.tipsChange > 0 && thisObject.tipsShop > 0)
                    {
                        int digit = Random.Range(0, 101);

                        if (digit <= thisObject.tipsChange)
                        {
                            GameManager.instance.GetMoney(thisObject.incomeShop * (thisObject.tipsChange / 100), "popup");
                        }

                    }


                    //get money
                    GameManager.instance.ShopGetMoney(thisObject.incomeShop, this, GameManager.instance.ckringClip);

                }
            }

        }

    }


    IEnumerator spawnNpc(int indexPromo)
    {
        //get npc random in this promo
        int npcIndex = Random.Range(0, thisObject.promo[indexPromo].promoObj.npcCall.Length);

        //setting data to variabel
        GameObject prefab = thisObject.promo[indexPromo].promoObj.npcCall[npcIndex].prefabs;
        float spawnMinTime = thisObject.promo[indexPromo].promoObj.npcCall[npcIndex].spawnMinTime;
        float spawnMaxTime = thisObject.promo[indexPromo].promoObj.npcCall[npcIndex].spawnMaxTime;
        int currLvl = thisObject.promo[indexPromo].currentLvl;

        //wait npc delay
        yield return new WaitForSeconds(Random.Range(spawnMinTime, spawnMaxTime) - thisObject.promo[indexPromo].spawnSpeedPercentage);

        //random spawn from left or right
        int randomDirection = Random.Range(0, 100);
        Transform npcSpawnPoint = randomDirection > 50 ? gm.npcLeftPoint : gm.npcRightPoint;

        //clone npc
        Npc npc = Instantiate(prefab, npcSpawnPoint.position, Quaternion.identity, npcSpawnPoint).GetComponent<Npc>();
        npc.targetShop = thisObject;
        npc.thisNpc = thisObject.promo[indexPromo].promoObj.npcCall[npcIndex];


        //set npc walk direction
        if (randomDirection > 50)
        {
            npc.walkingDirection = Vector2.right;
            npc.transform.localScale = new Vector2(-npc.transform.localScale.x, npc.transform.localScale.y);

        }
        else
        {
            npc.walkingDirection = Vector2.left;
        }

        StartCoroutine(spawnNpc(indexPromo));

    }

}

