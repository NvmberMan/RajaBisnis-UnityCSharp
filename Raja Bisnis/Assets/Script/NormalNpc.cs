using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalNpc : Npc
{
    private void Start()
    {
        CheckTarget();
    }
    private void Update()
    {
        NpcWalking();

        if(goToTarget)
        {
            NpcWalkToShop();
        }
    }



}
