using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    public int ID;

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Player")
        {
            if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Action"]))
            {
                GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().Save(ID);
            }
        }
    }
}
