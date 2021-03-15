using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public static EventController instance = null;
    public string currentDay;
    public Events allEvents;

    public GameObject eventTitle;
    public GameObject eventDetail;
    public GameObject choice1;
    public UnityAction action1;
    public GameObject choice2;
    public GameObject choice3;

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        BindEventUI();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        allEvents = LoadJson.LoadJsonFromFile<Events>();
        
        currentDay = "2021-01-03";
        
        CheckEvents();
        // currentDay = "2021-01-04";
        // CheckEvents();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckEvents()
    {
        foreach (Event e in allEvents.events)
        {
            if (e.time == currentDay)
            {
                NewEvent(e.time);
            }
        }
    }

    public void NewEvent(string day)
    {
        Event currentEvent = new Event();
        foreach (Event e in allEvents.events)
        {
            if (e.time == day)
            {
                currentEvent = e;
            }
        }

        eventTitle.GetComponent<Text>().text = currentEvent.title;
        eventDetail.GetComponent<Text>().text = currentEvent.text;
        
        if (currentEvent.choice1 == "")
        {
            choice1.SetActive(false);
        }
        else
        {
            choice1.SetActive(true);
            action1 = () => { eventCallBack(choice1, currentEvent.target1, currentEvent.influence1); };
            choice1.GetComponent<Button>().onClick.AddListener(action1);
            choice1.GetComponentInChildren<Text>().text = currentEvent.choice1;
        }
        
        if (currentEvent.choice2 == "")
        {
            choice2.SetActive(false);
        }
        else
        {
            choice2.SetActive(true);
            choice2.GetComponentInChildren<Text>().text = currentEvent.choice2;
        }

        if (currentEvent.choice3 == "")
        {
            choice3.SetActive(false);
            
        }
        else
        {
            choice3.SetActive(true);
            choice3.GetComponentInChildren<Text>().text = currentEvent.choice3;
        }
    }

    private void eventCallBack(GameObject choice, string target, double influence)
    {
        Debug.Log(target + " " + influence);
        choice.GetComponent<Button>().onClick.RemoveListener(action1);

    }


    private void BindEventUI()
    {
        eventTitle = GameObject.Find("Title");
        eventDetail = GameObject.Find("EventDetail");
        choice1 = GameObject.Find("Choice1");
        choice2 = GameObject.Find("Choice2");
        choice3 = GameObject.Find("Choice3");
    }

}
