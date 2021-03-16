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

    public GameObject eventBox;
    public GameObject eventTitle;
    public GameObject eventDetail;

    public GameObject choice1;
    public UnityAction action1;

    public GameObject choice2;
    public UnityAction action2;

    public GameObject choice3;
    public UnityAction action3;

    
    public void Awake()
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
        
    }

    public void CheckEvents()
    {
        foreach (Event e in allEvents.events)
        {
            if (e.time == GameController.instance.currentDate)
            {
                NewEvent(e.time);
            }
        }
    }

    public void NewEvent(string day)
    {
        // stop time pass
        GameController.instance.stopped = true;
        // show event gameObject
        Debug.Log(eventTitle);
        eventBox.SetActive(true);

        Event currentEvent = new Event();
        Debug.Log(day);
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
            action1 = () => { eventCallBack(choice1, currentEvent.target1, currentEvent.influence1, "action1"); };
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
            action2 = () => { eventCallBack(choice2, currentEvent.target2, currentEvent.influence2, "action2"); };
            choice2.GetComponent<Button>().onClick.AddListener(action2);
            choice2.GetComponentInChildren<Text>().text = currentEvent.choice2;
        }

        if (currentEvent.choice3 == "")
        {
            choice3.SetActive(false);
            
        }
        else
        {
            choice3.SetActive(true);
            action3 = () => { eventCallBack(choice3, currentEvent.target3, currentEvent.influence3, "action3"); };
            choice3.GetComponent<Button>().onClick.AddListener(action3);
            choice3.GetComponentInChildren<Text>().text = currentEvent.choice3;
        }
    }

    private void eventCallBack(GameObject choice, string target, double influence, string a)
    {
        
        // Debug.Log(target + " " + influence);
        if (target == "case")
        {
            GameController.instance.casePer += (float)influence;
        }
        else if (target == "money")
        {
            GameController.instance.moneyPer += (float)influence;
        }
        else if (target == "casemoney")
        {
            GameController.instance.casePer += (float)influence;
            GameController.instance.moneyPer += (float)influence;
        }
        else if (target == "moneycase")
        {
            GameController.instance.casePer -= (float)influence;
            GameController.instance.moneyPer -= (float)influence;
        }
        else if (target == "pro")
        {
            GameController.instance.moneyPer -= (float)influence;
            PolicyController.instance.pro += 1f;
        }
        else if (target == "vac")
        {
            GameController.instance.moneyPer -= (float)influence;
            PolicyController.instance.vac += 1f;
        }

        if (a == "action1")
        {
            choice.GetComponent<Button>().onClick.RemoveListener(action1);
        }
        else if (a == "action2")
        {
            choice.GetComponent<Button>().onClick.RemoveListener(action2);
        }
        else if (a == "action3")
        {
            choice.GetComponent<Button>().onClick.RemoveListener(action3);
        }
        else 
        {
            Debug.LogError("No such action!");
        }
        
        eventBox.SetActive(false);
        GameController.instance.stopped = false;

    }


    public void BindEventUI()
    {
        eventBox = GameObject.Find("EventBox");
        eventTitle = GameObject.Find("Title");
        eventDetail = GameObject.Find("EventDetail");
        choice1 = GameObject.Find("Choice1");
        choice2 = GameObject.Find("Choice2");
        choice3 = GameObject.Find("Choice3");
    }

}
