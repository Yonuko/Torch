using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{

    [Serializable]
    public struct eventsData
    {
        public int day;
        public bool isAtASpecificTime;
        public int startHour;
        public int endHour;
        public UnityEvent eventFunction;
    }

    public List<eventsData> eventsList = new List<eventsData>();

    // Start is called before the first frame update
    void Start()
    {
        eventsList[0].eventFunction.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show1()
    {
        Debug.Log("Tiens");
    }

    public void Show2()
    {
        Debug.Log("re tiens");
    }

    public void Show3()
    {
        Debug.Log("Tiens encore");
    }
}
