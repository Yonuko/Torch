using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour, IDropHandler {

    public int SlotNumber;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItems DroppedItem = eventData.pointerDrag.GetComponent<InventoryItems>();

        if (InventoryManager.inventoryManager.items[SlotNumber] == -1)
        {
            InventoryManager.inventoryManager.items[DroppedItem.slotIndex] = -1;
            DroppedItem.slotIndex = SlotNumber;
            InventoryManager.inventoryManager.items[DroppedItem.slotIndex] = DroppedItem.id;
        }
        else
        {
            Transform Item = transform.GetChild(0);
            Item.GetComponent<InventoryItems>().slotIndex = DroppedItem.slotIndex;
            Item.transform.SetParent(GameObject.FindWithTag("ItemList").transform.GetChild(DroppedItem.slotIndex).transform);
            Item.transform.position = GameObject.FindWithTag("ItemList").transform.GetChild(DroppedItem.slotIndex).transform.position;
            InventoryManager.inventoryManager.items[DroppedItem.slotIndex] = Item.GetComponent<InventoryItems>().id;

            DroppedItem.slotIndex = SlotNumber;
            DroppedItem.transform.SetParent(GameObject.FindWithTag("ItemList").transform.GetChild(DroppedItem.slotIndex).transform);

            InventoryManager.inventoryManager.items[SlotNumber] = DroppedItem.id;
        }
    }
}
