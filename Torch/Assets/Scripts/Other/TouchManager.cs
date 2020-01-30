using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public GameObject pauseObject;

    bool pause;

    Loader loader;

    MapManager mapManager;

    // Use this for initialization
    void Start () {
        pauseObject = GameObject.Find("Pause");

        loader = GameObject.FindWithTag("Loader").GetComponent<Loader>();

        mapManager = FindObjectOfType<MapManager>();

    }
	
	// Update is called once per frame
	void Update () {
        // Temporaire
        if (Input.GetKeyDown(KeyCode.A))
        {
            InventoryManager.inventoryManager.AddItems(1, 1);
        }
        //---------------------------

        if (Input.GetKeyDown(loader.datas.keys["Escape"]) && !mapManager.isActive && !InventoryManager.inventoryManager.inventory )
        {
            pause = !pause;
        }
        else if (FindObjectOfType<MapManager>().isActive)
        {
            pause = false;
        }
        else if (InventoryManager.inventoryManager.inventory && Input.GetKeyDown(loader.datas.keys["Escape"]))
        {
            InventoryManager.inventoryManager.inventory = false;
        }

        pauseObject.SetActive(pause);

        if(pause)
        {
            Time.timeScale = 0;
            Camera.main.GetComponent<CameraController>().enabled = false;
        }
        else if(Camera.main)
        {
            Time.timeScale = 1;
            Camera.main.GetComponent<CameraController>().enabled = true;
        }

	}

    public void ClosePausePanel()
    {
        pause = false;
    }
}
