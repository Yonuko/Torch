using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject mapHolder, map3D, map2D, minZoom, maxZoom, map3DPointerHolder;
    public Camera mapCam;

    GameObject mainCam;

    public float camSpeed, waitBeforeOpen, camZoomDistance;

    public bool isActive, is3DMap;

    private bool stopTraveling = true;

    Loader l;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main.gameObject;

        if (Loader.IsLoader())
        {
            l = Loader.get();
        }

    }
	
	// Update is called once per frame
	void Update () {

        map3DPointerHolder.SetActive(is3DMap);

        if (Input.GetKeyDown(l.datas.keys["Map"]) && !mapHolder.activeSelf)
        {
            StartCoroutine(WaitAnimationToEnd());
            isActive = true;
        }

        if (mapHolder.activeSelf)
        {
            if (is3DMap)
            {
                map3D.SetActive(true);
                map2D.SetActive(false);
            }
            else
            {
                map3D.SetActive(false);
                map2D.SetActive(true);
            }
        }

        if (mapHolder.activeSelf && stopTraveling)
        {
            mapCam.transform.LookAt(maxZoom.transform);
            mapCam.transform.Translate(Vector3.forward * camSpeed);
        }
        else if (mapHolder.activeSelf)
        {
            if (Input.GetKeyDown(l.datas.keys["Escape"]) || Input.GetKeyDown(l.datas.keys["Map"]))
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMouvement>().enabled = true;
                mainCam.SetActive(true);
                mapCam.transform.position = minZoom.transform.position;
                mapHolder.SetActive(false);
                StartCoroutine(WaitToPause());
            }
        }

        camZoomDistance = Vector3.Distance(mapCam.transform.position,maxZoom.transform.position);

        if (camZoomDistance < 0.05f)
        {
            stopTraveling = false;
        }
        else
        {
            stopTraveling = true;
        }

	}

    IEnumerator WaitToPause()
    {
        yield return new WaitForSeconds(0.2f * Time.deltaTime);
        isActive = false;
    }

    IEnumerator WaitAnimationToEnd()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMouvement>().enabled = false;
        yield return new WaitForSeconds(waitBeforeOpen);
        mainCam.SetActive(false);
        mapHolder.SetActive(true);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.transform.tag == "Map")
        {
            Destroy(hit.gameObject);
            is3DMap = true;
        }
    }
}
