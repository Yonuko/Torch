using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
                                             IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id, slotIndex, itemAmount;
    Image icon;

    public Text count;

    bool selected;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();

        for (int i = 0; i < ItemsManager.itemsManager.itemsList.Count; i++)
        {
            if (ItemsManager.itemsManager.itemsList[i].id == id)
            {
                icon.sprite = ItemsManager.itemsManager.itemsList[i].image;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Drop"]) && selected)
        {
            ItemsManager.itemsManager.DropItem(1, slotIndex);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(1))
        {
            transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            for (int i = 0; i < ItemsManager.itemsManager.itemsList.Count; i++)
            {
                if (ItemsManager.itemsManager.itemsList[i].id == id)
                {
                    ItemsManager.itemsManager.UseItem(ItemsManager.itemsManager.itemsList[i], slotIndex);
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(GameObject.FindWithTag("ItemList").transform.GetChild(slotIndex).transform);
        transform.position = GameObject.FindWithTag("ItemList").transform.GetChild(slotIndex).transform.position;
        transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(1))
        {
            transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(1))
        {
            transform.position = eventData.position;
            // transform.SetParent(InventoryUI.inventoryUI.SlotPanel.transform);
            transform.SetParent(transform.root.GetComponent<InventoryUI>().itemsPanel.transform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(1))
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(GameObject.FindWithTag("ItemList").transform.GetChild(slotIndex).transform);
            transform.position = GameObject.FindWithTag("ItemList").transform.GetChild(slotIndex).transform.position;
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selected = false;
    }
}
