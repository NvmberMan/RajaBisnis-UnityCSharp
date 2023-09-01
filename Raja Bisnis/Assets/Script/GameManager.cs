using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() { instance = this; }
    [Header("Game Data")]
    private string moneyString;

    public double money;
    public int day;
    [Range(0, 1440)]
    public float time;

    [Header("System Npc")]
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;

    [Space(10)]
    public Transform npcLeftPoint;
    public Transform npcRightPoint;
    public List<NpcItem> npcPrefabs = new List<NpcItem>();

    [Header("System Audio")]
    public AudioClip ckringClip;
    public AudioClip plungClip;
    public AudioClip popupClip;
    public GameObject soundEfxPrefab;

    [Header("System UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] public Canvas canvasGame;

    [Space(5)]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text timeText;

    [Space(5)]
    [SerializeField] private Transform alertContainer;

    [Space(5)]
    [SerializeField] private GameObject popupMoneyEFX;
    [SerializeField] private GameObject specialMoneyEFX;
    [SerializeField] private GameObject containerEFX;
    [SerializeField] private GameObject alertEfx;

    [Header("Ruko Manager")]
    public ShopObject currentShopSelected;
    [Space(5)]
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text capacityText;
    [SerializeField] private TMP_Text servingTimeText;
    [SerializeField] private TMP_Text tipsText;
    [SerializeField] private TMP_Text incomeText;

    [Space(10)]
    [SerializeField] private Image display;
    [SerializeField] private Slider experienceSlider;


    private void Start()
    {
        UpdateMoney();
        UpdateDay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetMoney(9352, "popup");
        }
        TimeSystem();
    }

    public void TimeSystem()
    {
        int menit = Mathf.FloorToInt(time / 60);
        int detik = Mathf.FloorToInt(time % 60);

        time += 4 * Time.deltaTime;


        timeText.text = menit.ToString("00") + " : " + detik.ToString("00");

        if (time >= 1440)
        {
            day++;
            time = 0;
            UpdateDay();
        }
    }

    public void UpdateDay()
    {
        dayText.text = "Day " + day.ToString();
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

    public void ShopGetMoney(double value, Shop shop, AudioClip clip = null)
    {
        GameObject moneyEfx = Instantiate(popupMoneyEFX, popupMoneyEFX.transform.position, Quaternion.identity, shop.uiTransform);
        moneyEfx.transform.localPosition = Vector3.zero;
        moneyEfx.GetComponent<PopupEFX>().value = value;

        if(clip)
        {
            moneyEfx.GetComponent<PopupEFX>().clip = clip;
        }

        Destroy(moneyEfx, 1.2f);

        money += value;
        UpdateMoney();
    }
    public void UpdateMoney()
    {
        moneyString = SFNuffix.GetShortValue(money, 1);
        moneyText.text = moneyString;
    }


    //Open RukoManager or Update.... when we click in selection menu
    public void updateRukoManager()
    {

        if (currentShopSelected != null)
        {
            EmployeeManager.instance.updateEmployee();
            EquipmentManager.instance.updateEquipment();
            PromoManager.instance.updatePromo();

            display.sprite = currentShopSelected.displayShop;
            //servingTimeText.text = currentShopSelected.servingTimeShop.ToString("F1") + " ms";
            incomeText.text = SFNuffix.GetShortValue(currentShopSelected.incomeShop, 1) + " / Hari";

            updateExpererience();
        }

    }

    public void updateExpererience()
    {
        if(currentShopSelected.lvlShop <= currentShopSelected.expShop.Count)
        {
            int max = currentShopSelected.expShop[currentShopSelected.lvlShop - 1].max;
            int exp = currentShopSelected.expShop[currentShopSelected.lvlShop - 1].exp;
            int sisa = 0;

            if (exp >= max)
            {
                sisa = exp - max;
                currentShopSelected.lvlShop += 1;
                currentShopSelected.expShop[currentShopSelected.lvlShop - 1].exp += sisa;

                NavContent.instance.updateMenu();
            }

            experienceSlider.maxValue = max;
            experienceSlider.value = exp;

            lvlText.text = "Lvl." + currentShopSelected.lvlShop.ToString();
        }


    }



    public void showAlert(string text, float delay = 2)
    {
        AlertUI a = Instantiate(alertEfx, alertContainer).GetComponent<AlertUI>();
        a.text = text;
        a.destroyDelayed = delay;
    }

    public void spawnSoundEfx(AudioClip clip, float destroy = 2, float volume = 1)
    {
        AudioSource soundEfx = Instantiate(soundEfxPrefab).GetComponent<AudioSource>();
        soundEfx.clip = clip;
        soundEfx.volume = volume;
        soundEfx.Play();
        Destroy(soundEfx.gameObject, destroy);
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