using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public GameObject pauseObject;

    bool pause;

	// Use this for initialization
	void Start () {
        pauseObject = GameObject.Find("Pause");
	}
	
	// Update is called once per frame
	void Update () {

        //Temporaire
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject.FindWithTag("Player").transform.position = GameObject.Find("teleportPoint").transform.position;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InventoryManager.inventoryManager.AddItems(1, 1);
        }
        //---------------------------

        if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Escape"]) && !FindObjectOfType<MapManager>().isActive && !InventoryManager.inventoryManager.inventory)
        {
            pause = !pause;
        }
        else if (FindObjectOfType<MapManager>().isActive)
        {
            pause = false;
        }
        else if (InventoryManager.inventoryManager.inventory && Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Escape"]))
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
