using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public GameObject slot, item;

    public GameObject itemsPanel, inventoryUI;

    PlayerSaveLoader dp;

    // Start is called before the first frame update
    void Start()
    {
        // Fait spawn le nombre voulu de cases
        for (int i = 0; i < InventoryManager.inventoryManager.slotCount; i++)
        {
            GameObject go = Instantiate(slot, itemsPanel.transform);
            go.transform.localScale = new Vector3(1,1,1);
            go.GetComponent<ItemSlots>().SlotNumber = i;
        }

        if (PlayerSaveLoader.IsPlayerLoader())
        {
            dp = GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>();
        }

        if (dp.datas.items.Count != 0)
        {
            for (int i = 0; i < dp.datas.items.Count; i++)
            {
                if (dp.datas.items[i] != -1)
                {
                    AddItem(i, dp.datas.items[i], true);
                    itemsPanel.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>().slotIndex = i;
                    for (int j = 0; j < dp.datas.itemsAmount[i] - 1; j++)
                    {
                        GameObject.FindWithTag("Player").transform.GetChild(4).GetComponent<InventoryManager>().AddItems(dp.datas.items[i], dp.datas.itemsAmount[i]);
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Affiche l'inventaire
        inventoryUI.SetActive(InventoryManager.inventoryManager.inventory);
    }

    public void AddItem(int position, int id, bool newItem = false)
    {
        if (newItem == true)
        {
            GameObject go = Instantiate(item, itemsPanel.transform.GetChild(position));
            go.GetComponent<InventoryItems>().id = id;
            go.GetComponent<InventoryItems>().itemAmount = 1;
            go.GetComponent<InventoryItems>().count.text = "";
        }
        else
        {
            InventoryItems currentItem = itemsPanel.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>();
            currentItem.count.text = currentItem.itemAmount.ToString();
        }
    }

    public void RemoveItem(int itemAmount, int position)
    {
        if (itemAmount > 1)
        {
            // Show the count of this item in inventory
            itemsPanel.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().count.text = itemAmount.ToString();
        }
        else if(itemAmount == 1)
        {
            // Disable the count text if there is only 1 item of that type
            itemsPanel.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().count.text = "";
        }
        else
        {
            // Remove the item if there is no more item
            Destroy(itemsPanel.transform.GetChild(position).GetChild(0).gameObject);
        }
    }
}
