using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Base Npc")]
    public ShopObject targetShop;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject graphic;

    [HideInInspector]public Vector2 walkingDirection;
    [HideInInspector] public bool stopWalking, goToTarget, isTemp;

    private RectTransform rectTransform, doorRect;
    private float dist;

    Transform target, doorTarget;
    Animator anim;
    Shop sh;
    public virtual void CheckTarget()
    {
        anim = graphic.GetComponent<Animator>();
        if (targetShop)
        {
            RukoManager rm = RukoManager.instance;
            for (int i = 0; i < rm.shopScenes.Count; i++)
            {
                if (rm.shopScenes[i].thisObject == targetShop)
                {
                    //checking distance of shop with npc
                    sh = rm.shopScenes[i];
                    target = sh.targetPoint[Random.Range(0, sh.targetPoint.Length)];
                    doorTarget = sh.doorPoint[Random.Range(0, sh.doorPoint.Length)];

                    if(targetShop.capacity_Temp < targetShop.capacityMax)
                    {
                         targetShop.capacity_Temp += 1;
                        isTemp = true;
                    }
                    break;
                }
            }
        }
    }

    public virtual void NpcWalking()
    {
        if(!stopWalking)
        {
            rectTransform = GetComponent<RectTransform>();
            Vector2 position = rectTransform.anchoredPosition;

            if (walkingDirection == Vector2.right)
            {
                position.x += moveSpeed * Time.deltaTime;
            }
            else
            {
                position.x -= moveSpeed * Time.deltaTime;
            }

            rectTransform.anchoredPosition = position;

            if (walkingDirection == Vector2.right && transform.position.x >= GameManager.instance.npcRightPoint.position.x)
            {
                Destroy(this.gameObject);
            }
            else if (walkingDirection == Vector2.left && transform.position.x <= GameManager.instance.npcLeftPoint.position.x)
            {
                Destroy(this.gameObject);
            }
        }


        //checking npc if want to buy something
        if (target)
        {
            if(targetShop.capacity_Temp < targetShop.capacityMax || isTemp)
            {
                dist = Vector2.Distance(target.position, transform.position);

                if (dist <= 2 && isTemp)
                {
                    goToTarget = true;
                    stopWalking = true;
                }
            }

        }

    }

    bool insideShop = false;
    public virtual void NpcWalkToShop()
    {
        rectTransform = GetComponent<RectTransform>();
        doorRect = doorTarget.GetComponent<RectTransform>();
        rectTransform.position = Vector2.MoveTowards(transform.position, doorTarget.position, 4 * Time.deltaTime);

        anim.Play("GirlWalk_Front");

        if(Vector2.Distance(transform.position, doorTarget.position) < 1 && !insideShop)
        {
            //Menu m = targetShop.menu[Random.Range(0,targetShop.menu.Count)];
            //UpgradeItem up = m.upgradeItem[m.currLevel - 1];

            //GameManager.instance.GetMoney(up.income, "popup");

            targetShop.capacityShop += 1;
            sh.updateCapacity();

            Destroy(this.gameObject);
            insideShop = true;
        }
    }
}
