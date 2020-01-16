using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{

    public static DayNightCycleManager dayNightCycle;
    float currentDayTime = 0, currentStateTime = 0;
    [SerializeField]
    private int maxDayTime;

    bool isDayTime = true;
    int currentDay = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if (dayNightCycle != null)
        {
            Destroy(gameObject);
            return;
        }
        dayNightCycle = this;
    }

    void Start()
    {
        currentDayTime = maxDayTime * 0.30f;
        currentStateTime = currentDayTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Add time while the seconds pass by
        currentDayTime += Time.deltaTime;
        currentStateTime += Time.deltaTime;
        if (currentStateTime >= maxDayTime)
        {
            currentStateTime = 0;
            isDayTime = !isDayTime;
        }
        if (currentDayTime >= 2 * maxDayTime)
        {
            currentDayTime = 0;
            currentDay++;
        }
    }

    public bool isDay()
    {
        return isDayTime;
    }

    public bool isNight()
    {
        return !isDayTime;
    }
    // Return the time of the moment
    public float GetCurrentTime()
    {
        return currentDayTime;
    }
    // Return the time of a full day
    public int GetMaxTimeInADay()
    {
        return maxDayTime;
    }
    // Return the number of the currentDay
    public int GetCurrentDayCount()
    {
        return currentDay;
    }
}