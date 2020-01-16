using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveLoader : MonoBehaviour
{

    const string fileName = "PlayerSave.Save";

    public GameObject player, ui;

    public PlayerDatas datas;

    static PlayerSaveLoader instance;

    GameObject slotHandler;

    void OnEnable()
    {

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (IsPlayerLoader())
        {
            Load();
        }

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (!arg0.name.Contains("Scene") || !IsPlayerLoader())
        {
            return;
        }

        Instantiate(ui, transform.position, transform.rotation);

        slotHandler = GameObject.FindWithTag("ItemList");

        if (datas.actualSavePoint != -1)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("SavePoint"))
            {
                if (go.GetComponent<SavePoint>().ID == datas.actualSavePoint)
                {
                    // Instantie le joueur
                    Instantiate(player, go.transform.position, go.transform.rotation);
                }
            }
        }
        else
        {
            Instantiate(player, GameObject.Find("Spawn").transform.position, GameObject.Find("Spawn").transform.rotation);
        }
    }

    void CreateSave()
    {
        datas = new PlayerDatas
        {
            actualSavePoint = -1,
            playerLife = 100,
            items = new List<int>(),
            itemsAmount = new List<int>()
        };

        PlayerDatasManager.Save(datas,fileName);
    }

    public void Save(int actualSavePoint)
    {
        datas = new PlayerDatas();

        datas.actualSavePoint = actualSavePoint;
        datas.playerLife = GameObject.FindWithTag("Player").GetComponent<PlayerLife>().playerLife;

        // Vérifie que l'objet InventoryManager se trouve toujours à la même place
        if (GameObject.FindWithTag("Player").transform.GetChild(4).GetComponent<InventoryManager>() == null)
        {
            Debug.LogError("L'objet InventoryManager n'est plus en 5 ème position, on ne peut accèder au script");
            return;
        }

        InventoryManager inventoryManager = GameObject.FindWithTag("Player").transform.GetChild(4).GetComponent<InventoryManager>();

        datas.items = inventoryManager.items;

        datas.itemsAmount = new List<int>();

        for (int i = 0; i < inventoryManager.slotCount; i++)
        {
            if (slotHandler.transform.GetChild(i).childCount != 0)
            {
                datas.itemsAmount.Add(slotHandler.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>().itemAmount);
            }
            else
            {
                datas.itemsAmount.Add(0);
            }
        }

        PlayerDatasManager.Save(datas,fileName);
    }

    public void Load()
    {
        datas = (PlayerDatas)PlayerDatasManager.Load(fileName);
    }

    public static bool IsPlayerLoader()
    {
        if (GameObject.FindWithTag("Loader") && File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            return true;
        }
        else if (!File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            Debug.LogError("File doesn't exist");
            return false;
        }
        else
        {
            Debug.LogError("PlayerLoader not found");
            return false;
        }
    }

    //Function UI----------------------------------------

    public void NewGameButton()
    {
        CreateSave();
        Camera.main.GetComponent<LoadingScreen>().LoadScene();
    }

    public void ContinueButton()
    {
        Camera.main.GetComponent<LoadingScreen>().LoadScene();
    }
}
