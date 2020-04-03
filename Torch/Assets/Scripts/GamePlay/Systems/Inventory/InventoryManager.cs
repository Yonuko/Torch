using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager inventoryManager;

    public int slotCount;

    public bool inventory;

    // ID des items contenu dans la case de rang i
    public List<int> items = new List<int>();

    GameObject itemListObject;

    Loader l;
    PlayerSaveLoader dp;

    // Start is called before the first frame update
    void Awake()
    {
        if (inventoryManager == null)
        {
            inventoryManager = this;
        }

        if (Loader.IsLoader())
        {
            l = Loader.get();
        }

        if (PlayerSaveLoader.IsPlayerLoader())
        {
            dp = GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>();
        }

        if (dp.datas.items.Count != 0)
        {
            items = dp.datas.items;
        }
        else
        {
            for (int i = 0; i < slotCount; i++)
            {
                items.Add(-1);
            }
        }
        itemListObject = GameObject.FindWithTag("UI").GetComponent<InventoryUI>().itemsPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(l.datas.keys["Inventory"]))
        {
            inventory = !inventory;
        }
    }

    int GetItemCountInInventory(int itemID)
    {
        if (!ContainsItem(itemID))
        {
            return 0;
        }
        int amount = 0;
        // Count all item Amount
        for (int i = 0; i < items.Count; i++)
        {
            if (itemListObject.transform.GetChild(i).childCount == 0)
            {
                continue;
            }
            InventoryItems inventoryItem = itemListObject.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>();
            amount += inventoryItem.itemAmount;
        }
        return amount;
    }

    public bool CanAddItems(int id, int count)
    {
        bool foundObject = false;

        List<Items> itemlist = ItemsManager.itemsManager.itemsList;
        Items currentItem = new Items();

        // Find the current item we want to add to now is maxAmount
        for (int i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i].id == id)
            {
                currentItem = itemlist[i];
            }
        }

        for (int i = 0; i < slotCount; i++)
        {
            if (items[i] == id)
            {
                // If the item isn't at his maxAmount, add 1 to the current slot
                InventoryItems itemInInventory = itemListObject.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>();
                if (itemInInventory.itemAmount < currentItem.maxAmount)
                {
                    foundObject = true;
                    return foundObject;
                }
            }
        }

        for (int i = 0; i < slotCount; i++)
        {
            if (items[i] == -1)
            {
                foundObject = true;
                break;
            }
        }
        return foundObject;
    }

    public void AddItems(int id, int count)
    {
        List<Items> itemlist = ItemsManager.itemsManager.itemsList;
        Items currentItem = new Items();

        // Find the current item we want to add to now is maxAmount
        for (int i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i].id == id)
            {
                currentItem = itemlist[i];
            }
        }

        // Check if we can add 1 more item to the slot existing
        bool canAdd = false;

        for (int i = 0; i < slotCount; i++)
        {
            if (items[i] == id)
            {
                // If the item isn't at his maxAmount, add 1 to the current slot
                InventoryItems itemInInventory = itemListObject.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>();
                if (itemInInventory.itemAmount < currentItem.maxAmount)
                {
                    canAdd = true;
                    itemInInventory.itemAmount++;
                    GameObject.FindWithTag("UI").GetComponent<InventoryUI>().AddItem(i, id);
                    break;
                }
            }
        }

        // Add a new item to the inventory
        if (!canAdd)
        {
            for (int i = 0; i < slotCount; i++)
            {
                if (items[i] == -1)
                {
                    items[i] = id;
                    GameObject.FindWithTag("UI").GetComponent<InventoryUI>().AddItem(i, id, true);
                    itemListObject.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>().slotIndex = i;
                    break;
                }
            }
        }

    }

    public void RemoveItems(int id, int count, int position)
    {
        // Remove 1 item
        itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount -= count;

        // Get the amount of the selected item
        int itemAmount = itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount;
        // Remove 1 item in the UI
        GameObject.FindWithTag("UI").GetComponent<InventoryUI>().RemoveItem(itemAmount, position);

        if (itemAmount == 0)
        {
            items[position] = -1;
        }
    }

    public void RemoveItems(int id, int count)
    {
        if (GetItemCountInInventory(id) < count)
        {
            Debug.LogError("Le nombre d'items que vous essayer de retirer est supérieur au nombre d'items contenus dans l'inventaire");
            return;
        }
        int position = 0;
        int amountToRemove = 0;

        if (count == 0)
        {
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == id)
            {
                position = i;
                if (itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount < count)
                {
                    count -= itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount;
                    amountToRemove = itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount;
                    break;
                }
                amountToRemove = count;
                count = 0;
                break;
            }
        }
        // Remove 1 item
        itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount -= amountToRemove;

        // Get the amount of the selected item
        int itemAmount = itemListObject.transform.GetChild(position).GetChild(0).GetComponent<InventoryItems>().itemAmount;
        // Remove 1 item in the UI
        GameObject.FindWithTag("UI").GetComponent<InventoryUI>().RemoveItem(itemAmount, position);

        if (itemAmount == 0)
        {
            items[position] = -1;
        }
        RemoveItems(id, count);
    }

    public bool ContainsItem(int itemID)
    {
        if (itemID < 0)
        {
            return false;
        }
        return items.Contains(itemID);
    }

    public bool ContainsItem(int[] itemID)
    {
        if (itemID.Length < 0)
        {
            return false;
        }
        for (int i = 0; i < itemID.Length; i++)
        {
            if (itemID[i] < 0)
            {
                return false;
            }
            if (!items.Contains(itemID[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool ContainsItem(List<int> itemID)
    {
        for (int i = 0; i < itemID.Count; i++)
        {
            if (itemID[i] < 0)
            {
                return false;
            }
            if (!items.Contains(itemID[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool ContainsItems(int itemID, int itemCount)
    {
        if (GetItemCountInInventory(itemID) < itemCount)
        {
            return false;
        }
        List<int> checkedPosition = new List<int>();
        int countOfItem = 0;
        for (int i = 0; i < items.Count && countOfItem < itemCount; i++)
        {
            if (items[i] == -1 || checkedPosition.Contains(i))
            {
                continue;
            }
            checkedPosition.Add(i);
            countOfItem += itemListObject.transform.GetChild(i).GetChild(0).GetComponent<InventoryItems>().itemAmount;
        }
        return countOfItem >= itemCount;
    }

    public List<int> GetTab()
    {
        return items;
    }
}
