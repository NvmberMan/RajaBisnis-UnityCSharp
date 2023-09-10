using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class LoadingGame : MonoBehaviour
{
    [SerializeField] private string URL = "localhost:3000";
    [SerializeField] private string savePath = "Assets/Images/";

    [SerializeField] private GameData gameData;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text loadingStatus;

    int new_versionId;
    string new_versionName;
    // Start is called before the first frame update
    void Start()
    {
        loadingSlider.maxValue = 100;
        StartCoroutine(FetchData());
    }

    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "/gamedata/version/" + gameData.versionId))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                loadingSlider.value = 20f;
                loadingStatus.text = "Loading..";
                Debug.Log("Checking Update..");



                yield return new WaitForSeconds(1f);
                loadingStatus.text = "Checking Update..";
                loadingSlider.value = 35f;


                if (CheckUpdate(request))
                {
                    Debug.Log("Updating Game..");
                    string newURL = URL + "/gamedata";


                    yield return new WaitForSeconds(0.8f); // Tambahkan delay satu detik di sini
                    StartCoroutine(FetchAndApplyUpdate(newURL));
                }
                else
                {
                    loadingStatus.text = "Loading Game...";
                    loadingSlider.value = 80f;


                    yield return new WaitForSeconds(0.5f);
                    loadingStatus.text = "Starting Game";
                    loadingSlider.value = 100f;

                    //load game scene
                    yield return new WaitForSeconds(0.2f);
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }

    private IEnumerator FetchAndApplyUpdate(string newURL)
    {
        using (UnityWebRequest updateRequest = UnityWebRequest.Get(newURL))
        {
            yield return updateRequest.SendWebRequest();
            if (updateRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(updateRequest.error);
            }
            else
            {
                UpdateGame(updateRequest);
                loadingStatus.text = "Loading Game...";
                loadingSlider.value = 90f;


                yield return new WaitForSeconds(0.5f);
                loadingStatus.text = "Starting Game";
                loadingSlider.value = 100f;

                //load game scene
                yield return new WaitForSeconds(0.2f);
                //SceneManager.LoadScene("Game");
            }
        }
    }



    bool CheckUpdate(UnityWebRequest request)
    {
        string t = request.downloadHandler.text;
        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(t);

        if (json["update"])
        {
            new_versionId = json["new_version"]["id"];
            new_versionName = json["new_version"]["update_name"];
        }
        return json["update"];
    }

    void UpdateGame(UnityWebRequest request)
    {
        string t = request.downloadHandler.text;
        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(t);

        gameData.Shop.Clear();

        foreach (SimpleJSON.JSONNode shopData in json["data"])
        {
            List<Data_Menu> m = new List<Data_Menu>();
            foreach (SimpleJSON.JSONNode menuData in shopData["menu"])
            {
                string menu_display_url = URL + "/menu/image/" + menuData["menu_display"];
                string menu_display_name = menuData["id"];
                StartCoroutine(DownloadAssets(menu_display_url, menu_display_name));

                Sprite sprite = Resources.Load<Sprite>("Menu/" + menu_display_name);
                m.Add(new Data_Menu()
                {
                    _id = menuData["id"],
                    _name = menuData["name"],
                    _description = menuData["description"],
                    _level_Max = menuData["level_max"],
                    _menu_sprite = sprite,

                    _price = menuData["price"],
                    _price_multiplier = menuData["price_multiplier"],
                    _price_unlock = menuData["price_unlock"],

                    _income = menuData["income"],
                    _income_multiplier = menuData["income_multiplier"],

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
        //gameData.versionId = new_versionId;
        gameData.versionName = new_versionName;
        Debug.Log("Successful update game");
    }


    //


    IEnumerator DownloadAssets(string imageUrl, string imageName)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Mendapatkan teksur gambar
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;


            // Membuat path lengkap untuk menyimpan gambar
            string fullPath = savePath + imageName + ".png"; // Anda bisa menyimpannya dalam format yang sesuai

            // Menyimpan teksur gambar ke dalam folder Assets
            System.IO.File.WriteAllBytes(fullPath, texture.EncodeToPNG()); // Menggunakan PNG sebagai contoh

            // Refresh aset di Unity Editor
            UnityEditor.AssetDatabase.Refresh();

            Debug.Log("Gambar berhasil diunduh dan disimpan di: " + fullPath);

            
        }
        else
        {
            Debug.LogError("Gagal mengunduh gambar: " + www.error);
        }

    }
}
