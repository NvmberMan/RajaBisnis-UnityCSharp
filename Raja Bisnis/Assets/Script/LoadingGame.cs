using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class LoadingGame : MonoBehaviour
{
    public string URL = "localhost:3000";
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchData());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "/gamedata"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string t = request.downloadHandler.text;
                SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(t);

                gameData.Shop.Clear();

                foreach (SimpleJSON.JSONNode shopData in json["data"])
                {
                    List< Data_Menu> m = new List<Data_Menu>();
                    foreach(SimpleJSON.JSONNode menuData in shopData["menu"])
                    {
                        m.Add(new Data_Menu() { 
                            _id = menuData["id"],
                            _name = menuData["description"],
                            _level_Max = menuData["level_max"],
                            _price = menuData["price"],
                            _price_multiplier = menuData["price_multiplier"],
                            _price_unlock = menuData["price_unlock"],
                            _menu_display = menuData["menu_display"],
                            _id_Shop = menuData["shopId"]
                        });
                    }

                    gameData.Shop.Add(new Data_Shop()
                    {
                        _id = shopData["IdShop"],
                        _name = shopData["name"],
                        _description = shopData["description"],
                        _menu = m
                    });
                }


            }
        }

    }
}
