using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelLight : MonoBehaviour
{

    public Transform[] lodObject = new Transform[3];
    bool nightIsDone = false, dayIsDone = false;
    DayNightCycleManager dayNightCycleManager;
    Color color = new Color(191, 191, 191);

    // Start is called before the first frame update
    void Start()
    {
        dayNightCycleManager = GameObject.FindWithTag("GameController").GetComponent<DayNightCycleManager>();
        for (int i = 0; i < 3; i++)
        {
            lodObject[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNightCycleManager.isNight() && !nightIsDone)
        {
            nightIsDone = true;
            dayIsDone = false;
            foreach (Transform currentObject in lodObject)
            {
                for (int i = 0; i < currentObject.childCount; i++)
                {
                    currentObject.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
        }
        else if (!dayNightCycleManager.isNight() && !dayIsDone)
        {
            nightIsDone = false;
            dayIsDone = true;
            foreach (Transform currentObject in lodObject)
            {
                for (int i = 0; i < currentObject.childCount; i++)
                {
                    currentObject.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}
