using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEFX : EFX
{
    [HideInInspector]public double value;

    public void Update()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                GameManager.instance.GetMoney(value);
                Destroy(this.gameObject);
            }
        }
    }
}
