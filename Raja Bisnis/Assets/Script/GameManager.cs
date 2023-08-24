using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() { instance = this; }
    [Header("Game Data")]
    private string moneyString;
    public double money;

    [Header("System Npc")]
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;

    [Space(10)]
    public Transform npcLeftPoint;
    public Transform npcRightPoint;
    public List<Npc> npcPrefabs = new List<Npc>();

    [Header("System UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas canvasGame;

    [Space(5)]
    [SerializeField] private TMP_Text moneyText;

    [Space(5)]
    [SerializeField] private GameObject popupMoneyEFX;
    [SerializeField] private GameObject specialMoneyEFX;
    [SerializeField] private GameObject containerEFX;


    private void Start()
    {
        UpdateMoney();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetMoney(9352, "popup");
        }
    }

    public void GetMoney(double value, string animation = "none")
    {
        

        Vector2 containerX = Mechanic.GetContainerSize_X(containerEFX);
        Vector2 containerY = Mechanic.GetContainerSize_Y(containerEFX);

        if (animation == "popup")
        {
            GameObject moneyEfx = Instantiate(popupMoneyEFX, 
                new Vector2(
                    Random.Range(containerX.x, containerX.y),
                    Random.Range(containerY.x, containerY.y)
                ),
            Quaternion.identity, containerEFX.transform);
            moneyEfx.GetComponent<PopupEFX>().value = value;

            Destroy(moneyEfx, 1.2f);

            money += value;
            UpdateMoney();

        }
        else if(animation == "special")
        {
            int loopCount = Mathf.FloorToInt((float)(value < 30 ? value : 30));
            float lastSpeed = 0;
            for (int i = 0; i < loopCount; i++)
            {
                GameObject moneyEfx = Instantiate(specialMoneyEFX,
                    new Vector2(
                        Random.Range(containerX.x, containerX.y),
                        Random.Range(containerY.x, containerY.y)
                    ),
                Quaternion.identity, containerEFX.transform);

                moneyEfx.GetComponent<SpecialEFX>().MoveDelaySmoothToTarget(2 + lastSpeed);
                moneyEfx.GetComponent<SpecialEFX>().target = moneyText.transform.parent.Find("Icon") ;
                moneyEfx.GetComponent<SpecialEFX>().value = value / loopCount;

                lastSpeed += 0.02f;
            }

        }
        else
        {
            money += value;
            UpdateMoney();
        }

    }
    public void UpdateMoney()
    {
        moneyString = SFNuffix.GetShortValue(money, 1);
        moneyText.text = moneyString;
    }


}

public static class Mechanic
{
    public static Vector2 GetContainerSize_X(GameObject obj)
    {
        return new Vector2(obj.transform.Find("MinX").transform.position.x, obj.transform.Find("MaxX").transform.position.x);
    }
    public static Vector2 GetContainerSize_Y(GameObject obj)
    {
        return new Vector2(obj.transform.Find("MinY").transform.position.y, obj.transform.Find("MaxY").transform.position.y);
    }
}