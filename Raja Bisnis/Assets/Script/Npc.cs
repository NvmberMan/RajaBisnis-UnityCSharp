using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Base Npc")]
    [SerializeField] private float moveSpeed = 3f;

    [HideInInspector]public Vector2 walkingDirection;
    public virtual void NpcWalking()
    {
        transform.Translate(walkingDirection * moveSpeed * Time.deltaTime);

        if(walkingDirection == Vector2.right && transform.position.x >= GameManager.instance.npcRightPoint.position.x)
        {
            Destroy(this.gameObject);
        }else if(walkingDirection == Vector2.left && transform.position.x <= GameManager.instance.npcLeftPoint.position.x)
        {
            Destroy(this.gameObject);
        }
    }
}
