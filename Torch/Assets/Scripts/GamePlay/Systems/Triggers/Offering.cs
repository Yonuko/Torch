using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offering : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (GameObject.FindWithTag("ItemManager").GetComponent<InventoryManager>().ContainsItem(1))
            {
                Debug.Log("LET THE RITUAL BEGINS!");
            }
        }
    }
}
