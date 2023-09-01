using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : MonoBehaviour
{
    public ShopObject shopObject;
    public GameObject[] lockedObject;
    public GameObject[] activeObject;
    public GameObject[] usedObject;
    [HideInInspector]public bool locked;
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        if (locked)
        {
            for (int i = 0; i < lockedObject.Length; i++)
            {
                lockedObject[i].SetActive(true);
            }
            for (int i = 0; i < activeObject.Length; i++)
            {
                activeObject[i].SetActive(false);
            }
        }
        else
        if (locked)
        {
            for (int i = 0; i < lockedObject.Length; i++)
            {
                lockedObject[i].SetActive(false);
            }
            for (int i = 0; i < activeObject.Length; i++)
            {
                activeObject[i].SetActive(true);
            }
        }

        if(used)
        {
            for (int i = 0; i < usedObject.Length; i++)
            {
                usedObject[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pilih()
    {
        CanvasManager.instance.pilihShop(shopObject);
        GameManager.instance.spawnSoundEfx(GameManager.instance.popupClip, 1, 0.5f);
    }
}
