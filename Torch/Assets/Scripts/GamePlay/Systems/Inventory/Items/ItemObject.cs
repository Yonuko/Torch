using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    bool getable = true, beenGot;

    public int id;

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.transform.tag == "Player" && getable && !beenGot)
        {
            beenGot = InventoryManager.inventoryManager.CanAddItems(id, 1);
            ItemsManager.itemsManager.PickUpItems(id, gameObject);
        }
    }

    public IEnumerator WaitToBeRecoltable()
    {
        getable = false;
        yield return new WaitForSeconds(1);
        getable = true;
    }
}
