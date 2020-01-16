using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class DayNightCycle : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<DayNightCycleManager>() != null)
        {
            dayNightCycleManager = GameObject.FindWithTag("GameController").GetComponent<DayNightCycleManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            (dayNightCycleManager.GetCurrentTime()) / dayNightCycleManager.GetMaxTimeInADay() * 180,
            0, 0);
        if (dayNightCycleManager.isNight())
        {
            GetComponent<Light>().intensity = 0.2f;
            RenderSettings.ambientIntensity = 0.2f;
        }
        else
        {
            GetComponent<Light>().intensity = 1;
            RenderSettings.ambientIntensity = 1;
        }
    }
}
