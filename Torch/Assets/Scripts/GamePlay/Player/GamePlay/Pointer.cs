using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            MapManager mapManager = GameObject.FindWithTag("Player").GetComponent<MapManager>();
            if (!mapManager.is3DMap)
            {
                GameObject.FindWithTag("Player").GetComponent<MapManager>().map2D.GetComponent<Map>().DeleteWayPoint(transform);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<MapManager>().map3D.GetComponent<Map>().DeleteWayPoint(transform);
            }
            Destroy(gameObject);
        }    
    }

}
