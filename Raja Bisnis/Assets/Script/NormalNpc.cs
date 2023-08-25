using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
