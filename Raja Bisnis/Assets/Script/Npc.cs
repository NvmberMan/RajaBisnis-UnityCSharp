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
    [HideInInspector] public bool stopWalking, goToTarget;
    [HideInInspector] public NpcItem thisNpc;
    [HideInInspector]public Transform roadTarget;

    private RectTransform rectTransform, doorRect;
    private float dist;

    Transform target;
    Transform  doorTarget;
    Animator anim;
    Shop sh;
    public virtual void CheckTarget()
    {
        anim = graphic.GetComponent<Animator>();

        //check if npc want to buy something
        if (targetShop)
        {
            RukoManager rm = RukoManager.instance;

            //searching shop to buy purpose
            for (int i = 0; i < rm.shopScenes.Count; i++)
            {

                if (rm.shopScenes[i].shop.thisObject == targetShop)
                {
                    //checking distance of shop with npc
                    sh = rm.shopScenes[i].shop;
                    target = sh.targetPoint[Random.Range(0, sh.targetPoint.Length)];

                    break;
                }
            }
        }
    }

    //npc walking left and right
    public virtual void NpcWalking()
    {
        if(!stopWalking)
        {
            anim.Play("Walk");
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
            //check distance between player with frontpoint
             dist = Vector2.Distance(target.position, transform.position);

             if (dist <= 1)
             {
                //goToTarget = true;
                //stopWalking = true;

                sh.thisObject.capacityNPC.Add(thisNpc);

                Destroy(this.gameObject);
                insideShop = true;
            }
            

        }

    }

    bool insideShop = false;
    //public virtual void NpcWalkToShop()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //    doorRect = doorTarget.GetComponent<RectTransform>();
    //    rectTransform.position = Vector2.MoveTowards(transform.position, doorTarget.position, 4 * Time.deltaTime);

    //    anim.Play("Walk_Front");

    //    //check distance between player with door
    //    if (Vector2.Distance(transform.position, doorTarget.position) < 1 && !insideShop)
    //    {
    //        //Menu m = targetShop.menu[Random.Range(0,targetShop.menu.Count)];
    //        //UpgradeItem up = m.upgradeItem[m.currLevel - 1];

    //        //GameManager.instance.GetMoney(up.income, "popup");

    //        sh.thisObject.capacityNPC.Add(thisNpc);

    //        Destroy(this.gameObject);
    //        insideShop = true;
    //    }
    //}


    public void NpcBackToRoad()
    {
        anim.Play("Walk_Back");

        rectTransform = GetComponent<RectTransform>();



        rectTransform.position = Vector2.MoveTowards(transform.position, roadTarget.position, 4 * Time.deltaTime);


        if(Vector2.Distance(transform.position, roadTarget.position) < 0.1f)
        {
            roadTarget = null;
        }
    }
}
