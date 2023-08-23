using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFX : MonoBehaviour
{
    [HideInInspector] bool move;
    [HideInInspector] public Transform target;
     public float speed;
    public virtual void MoveDelaySmoothToTarget(float delay)
    {
        Invoke("MoveSmoothToTarget", delay);
    }

    public virtual void MoveSmoothToTarget()
    {
        move = true;
    }
    public virtual void Move() {
        if(move)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
