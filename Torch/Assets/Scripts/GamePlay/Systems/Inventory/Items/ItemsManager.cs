using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager itemsManager;

    public List<Items> itemsList;

    void Awake()
    {
        if (itemsManager == null)
        {
            itemsManager = this;
        }
    }

    public void PickUpItems(int id, GameObject item)
    {
        // Si l'inventaire est complêt
        if (!InventoryManager.inventoryManager.CanAddItems(id, 1))
        {
            Text tipbox = FindObjectOfType<DialogueManager>().tipsBox;
            tipbox.text = "Vous ne pouvez pas récupérer cet item, votre inventaire est plein";
            tipbox.gameObject.SetActive(true);
            StartCoroutine(DisableTipBox(tipbox.gameObject));
            return;
        }
        StartCoroutine(WaitBeforePickUp(id, item));
    }

    public void UseItem(Items item, int position)
    {
        switch (item.type)
        {
            case Items.Type.Armor:
                // Ajoute l'armure sur le personnage
                break;
            case Items.Type.Consomable:
                // Utilise le consomable correspondant au type de l'item
                switch (item.consomableType)
                {
                    case Items.ConsomableType.healPotion:
                        break;
                    case Items.ConsomableType.manaPotion:
                        break;
                    case Items.ConsomableType.food:
                        break;
                    case Items.ConsomableType.speedScript:
                        break;
                }
                break;
            case Items.Type.Weapon:
                // Ajoute l'arme sur le personnage
                for (int i = 0; i < itemsList.Count; i++)
                {
                    if (itemsList[i].id == item.id)
                    {
                        GameObject go = Instantiate(itemsList[i].gameObject);
                        go.transform.SetParent(GameObject.FindWithTag("RightHand").transform);
                        go.transform.localPosition = new Vector3(0,0,0);
                        go.transform.localRotation = Quaternion.Euler(90,0,0);
                        foreach (Component comp in go.GetComponents<Collider>())
                        {
                            DestroyImmediate(comp);
                        }
                        DestroyImmediate(go.GetComponent<Rigidbody>());
                        InventoryManager.inventoryManager.RemoveItems(item.id, 1, position);
                        break;
                    }
                }
                break;
        }
    }

    public void DropItem(int id, int position)
    {
        for (int i = 0; i < itemsManager.itemsList.Count; i++)
        {
            if (itemsList[i].id == id)
            {
                GameObject go = Instantiate(itemsList[i].gameObject, GameObject.FindWithTag("Player").transform.position, Quaternion.Euler(90, 0, 0), GameObject.Find("Everything").transform);
                go.GetComponent<ItemObject>().StartCoroutine(go.GetComponent<ItemObject>().WaitToBeRecoltable());
                InventoryManager.inventoryManager.RemoveItems(id, 1, position);
            }
        }
    }

    public IEnumerator WaitBeforePickUp(int id, GameObject item)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMouvement>().enabled = false;
        player.GetComponent<Animator>().Play("PickUp");
        yield return new WaitForSeconds(1.6f);
        player.GetComponent<PlayerMouvement>().enabled = true;
        // ajoute l'objet à l'inventaire
        InventoryManager.inventoryManager.AddItems(id, 1);
        Destroy(item);
    }

    IEnumerator DisableTipBox(GameObject tipbox)
    {
        yield return new WaitForSeconds(2);
        tipbox.SetActive(false);
    }
}
