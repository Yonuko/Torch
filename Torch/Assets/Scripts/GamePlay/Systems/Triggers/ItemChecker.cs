using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    public List<int> neededItemList = new List<int>();      // List of needed items for the offering
    public List<int> numberItemList = new List<int>();      // Nombre de chaque item necessaire, même taille que neededItemList

    private List<int> missingItemList = new List<int>();    // List of missing items for the offering
    private int numberOfItemsPossessed = 0;                 // Number of necessary items that possesses the player

    private string stringTemp = "";                         // Temporary string
    Loader loader;

    private bool isEnabled = true;                          // Si la fonction est activée ou pas

    // Configurations
    public bool destroyItemsIfComplete = false;
    public bool triggerOnCollision = true;
    public bool triggerOnActionButton = false;
    public bool singleUseChecker = false;

    private void Start()
    {
        if (Loader.IsLoader())
        {
            loader = GameObject.FindWithTag("Loader").GetComponent<Loader>();
        }

        GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().AddItems(1,3);
        GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().AddItems(2, 3);
        GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().AddItems(3, 3);
    }

    private void Update()
    {
        if(triggerOnActionButton && !triggerOnCollision && isEnabled)
        {
            if (Input.GetKeyDown(loader.datas.keys["Action"]))
            {
                DoTheThing();
                DisableChecker();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (triggerOnCollision && isEnabled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if(triggerOnActionButton )
                {
                    if (Input.GetKeyDown(loader.datas.keys["Action"]))
                    {
                        DoTheThing();
                        DisableChecker();
                    }
                }
                else
                {
                    DoTheThing();
                    DisableChecker();
                }
            }
        }
    }

    private void DoTheThing()
    {
        for (int x = 0; x < neededItemList.Count; x++)
        {
            if (GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().ContainsItems(neededItemList[x], numberItemList[x]))
            {
                numberOfItemsPossessed++;
            }
            else
            {
                missingItemList.Add(neededItemList[x]);
            }
        }

        if (numberOfItemsPossessed >= neededItemList.Count)
        {
            Debug.Log("Conditions met!");
            DestroyItems();                 // Only active if the public bool is true
        }
        else
        {
            stringTemp = "Vous n'avez pas tous les éléments nécessaires au rituel. Les objets sont";
            for (int x = 0; x < missingItemList.Count; x++)
            {
                stringTemp += ", " + missingItemList[x];
            }
            Debug.Log(stringTemp);
        }
        numberOfItemsPossessed = 0;
        missingItemList.Clear();
    }

    private void DisableChecker()
    {
        if (singleUseChecker)
        {
            isEnabled = false;
        }
    }

    private void DestroyItems()
    {
        if (destroyItemsIfComplete)
        {
            for(int x =0; x<neededItemList.Count; x++)
            {
                GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().RemoveItems(x, numberItemList[x]);
            }
        }
    }
}
