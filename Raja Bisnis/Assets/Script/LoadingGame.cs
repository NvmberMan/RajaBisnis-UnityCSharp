using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class LoadingGame : MonoBehaviour
{
    [SerializeField] private string URL = "localhost:3000";
    //[SerializeField] private string savePath = "Assets/Images/";

    [SerializeField] private GameData gameData;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text loadingStatus, version;

    int new_versionId;
    string new_versionName;
    // Start is called before the first frame update
    void Start()
    {
        loadingSlider.maxValue = 100;
        gameData.Load();

        StartCoroutine(FetchData());
        version.text = "Version:" + gameData.versionId.ToString();
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
                loadingStatus.text = "Checking Update..";
                loadingSlider.value = 35f;


                if (CheckUpdate(request))
                {
                    string newURL = URL + "/gamedata";

                    loadingStatus.text = "Updating Game..";
                    StartCoroutine(DownloadAllAssets(newURL));
                }
                else
                {
                    loadingStatus.text = "Loading Game...";
                    loadingSlider.value = 40f;


                    yield return LoadGame(gameData.datajson);
                    yield return new WaitForSeconds(0.5f);
                    loadingSlider.value = 60f;

                    //load game scene
                    yield return new WaitForSeconds(1f);
                    loadingSlider.value = 100f;

                    loadingStatus.text = "Starting Game";
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }

    private IEnumerator DownloadAllAssets(string newURL)
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
                gameData.datajson = updateRequest.downloadHandler.text;
                yield return UpdateGame(gameData.datajson);

                //waiting ^^

                loadingStatus.text = "Loading Game...";
                loadingSlider.value = 85f;
                System.IO.File.WriteAllText(Application.persistentDataPath + "/version.json", new_versionId.ToString());


                yield return new WaitForSeconds(0.5f);
                loadingSlider.value = 95f;

                //load game scene
                yield return new WaitForSeconds(0.2f);
                loadingStatus.text = "Starting Game";
                loadingSlider.value = 95f;
                SceneManager.LoadScene("Game");
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

    IEnumerator UpdateGame(string jsonn)
    {

        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(jsonn);
        
        gameData.Shop.Clear();
        int count = 0;
        int countMax = json["data"].Count;
        foreach (SimpleJSON.JSONNode shopData in json["data"])
        {
            loadingStatus.text = "Updating " + count + "/" + countMax;
            List<Data_Menu> m = new List<Data_Menu>();
            foreach (SimpleJSON.JSONNode menuData in shopData["menu"])
            {
                //downloading menu image
                string menu_display_url = URL + "/menu/image/" + menuData["menu_display"];
                string menu_display_name = menuData["id"];
                loadingStatus.text = "Downloading " + menuData["name"];
                yield return StartCoroutine(DownloadAssets(menu_display_url, menu_display_name, "Assets/Resources/Menu/", null));
                loadingStatus.text = "Success Download " + menuData["name"];


                m.Add(new Data_Menu()
                {
                    _id = menuData["id"],
                    _name = menuData["name"],
                    _description = menuData["description"],
                    _level_Max = menuData["level_max"],

                    _price = menuData["price"],
                    _price_multiplier = menuData["price_multiplier"],
                    _price_unlock = menuData["price_unlock"],

                    _income = menuData["income"],
                    _income_multiplier = menuData["income_multiplier"],

                    _id_Shop = menuData["shopId"]
                });
            }
            Data_Shop ds = new Data_Shop();
            ds._id = shopData["IdShop"];
            ds._name = shopData["name"];
            ds._description = shopData["description"];
            ds._menu = m;

            //downloading shop image
            string shop_display_url = URL + "/menu/image/" + shopData["shop_display"];
            string shop_display_name = shopData["IdShop"];
            yield return StartCoroutine(DownloadAssets(shop_display_url, shop_display_name, "Assets/Resources/Shop/", ds));



            count++;
        }
        //gameData.versionId = new_versionId;
        gameData.versionName = new_versionName;
        Debug.Log("Successful update game");

        gameData.Save();
    }

 

    IEnumerator DownloadAssets(string imageUrl, string imageName, string path = "Assets/Resources/", Data_Shop data_shop = null)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Mendapatkan teksur gambar
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            loadingStatus.text = "Berhasil download texture";

            // Membuat path lengkap untuk menyimpan gambar
            string fullPath = path  + imageName + ".png"; // Anda bisa menyimpannya dalam format yang sesuai


            // Membuat path lengkap untuk menyimpan gambar di lokal penyimpanan perangkat
            string fullLocalPath = Application.persistentDataPath + "/" + imageName + ".png";

            // Menyimpan teksur gambar ke dalam lokal penyimpanan
            System.IO.File.WriteAllBytes(fullLocalPath, texture.EncodeToPNG());
            loadingStatus.text = "Berhasil Menyimpan ke local";


            byte[] data = ReadDataFromLocalStorage(imageName + ".png");
            loadingStatus.text = "Berhasil Membuat Byte";

            if (data != null)
            {
                // Buat folder Resources jika belum ada
                CreateResourcesFolder();
                loadingStatus.text = "Berhasil Membuat Folder Resources";

                // Pindahkan data ke folder Resources
                MoveDataToResourcesFolder(data, imageName + ".png");    
                loadingStatus.text = "Berhasil Memasukkan ke dalam resources";

            }

            // Refresh aset di Unity Editor
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif
            loadingStatus.text = "Berhasil Refresh Unity Editor";



            if (data_shop != null)
            {

                Sprite shop_prite = Resources.Load<Sprite>(imageName + ".png");
                data_shop._shop_sprite = shop_prite;

                foreach(Data_Menu menu in data_shop._menu)
                {
                    Sprite sprite = Resources.Load<Sprite>(imageName + ".png");

                    menu._menu_sprite = sprite;
                }
                gameData.Shop.Add(data_shop);
                loadingStatus.text = "Berhasil memasukkan sprite ke Gamedata";

            }
            //execution to data in Gamedata
        }
        else
        {
            Debug.LogError("Gagal mengunduh gambar: " + www.error);
            loadingStatus.text = www.error;
        }

    }





    byte[] ReadDataFromLocalStorage(string fileName)
    {
        string fullPath = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(fullPath))
        {
            return File.ReadAllBytes(fullPath);
        }
        else
        {
            Debug.LogError("File tidak ditemukan: " + fullPath);
            return null;
        }
    }

    void CreateResourcesFolder()
    {
        string resourcesPath = Application.dataPath + "/Resources";
        if (!Directory.Exists(resourcesPath))
        {
            Directory.CreateDirectory(resourcesPath);
        }
    }

    // Memindahkan data ke folder Resources
    IEnumerator MoveDataToResourcesFolder(byte[] data, string fileName)
    {
        string fullPath = Application.dataPath + "/Resources/" + fileName + ".png";
        File.WriteAllBytes(fullPath, data);
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
#endif

        yield return null;
    }





    IEnumerator LoadGame(string jsonn)
    {


        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(jsonn);

        gameData.Shop.Clear();

        foreach (SimpleJSON.JSONNode shopData in json["data"])
        {
            List<Data_Menu> m = new List<Data_Menu>();
            foreach (SimpleJSON.JSONNode menuData in shopData["menu"])
            {
                //load menu image
                string menu_display_name = menuData["id"];
                yield return LoadAssets(menu_display_name, null);

                m.Add(new Data_Menu()
                {
                    _id = menuData["id"],
                    _name = menuData["name"],
                    _description = menuData["description"],
                    _level_Max = menuData["level_max"],

                    _price = menuData["price"],
                    _price_multiplier = menuData["price_multiplier"],
                    _price_unlock = menuData["price_unlock"],

                    _income = menuData["income"],
                    _income_multiplier = menuData["income_multiplier"],

                    _id_Shop = menuData["shopId"]
                });
            }
            Data_Shop ds = new Data_Shop();
            ds._id = shopData["IdShop"];
            ds._name = shopData["name"];
            ds._description = shopData["description"];
            ds._menu = m;

            //load shop image
            string shop_display_name = shopData["IdShop"];
            yield return LoadAssets(shop_display_name, ds);




        }
        //gameData.versionId = new_versionId;
        gameData.versionName = new_versionName;
        Debug.Log("Successful load game");

        gameData.Save();
    }
    //
    IEnumerator LoadAssets(string imageName, Data_Shop data_shop = null)
    {


            byte[] data = ReadDataFromLocalStorage(imageName + ".png");

            if (data != null)
            {
                // Buat folder Resources jika belum ada
                CreateResourcesFolder();

                // Pindahkan data ke folder Resources
               yield return MoveDataToResourcesFolder(data, imageName + ".png");
            }

            // Refresh aset di Unity Editor
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif



            if (data_shop != null)
            {
                Sprite shop_prite = Resources.Load<Sprite>(imageName + ".png");
                data_shop._shop_sprite = shop_prite;

                foreach (Data_Menu menu in data_shop._menu)
                {
                    Sprite sprite = Resources.Load<Sprite>(imageName + ".png");

                    menu._menu_sprite = sprite;
                }
                gameData.Shop.Add(data_shop);

            }
            //execution to data in Gamedata


    }
}
