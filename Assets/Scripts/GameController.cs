using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{


    public static float timeForOneDay = 2.0f;
    public float timer = timeForOneDay;
    public string currentDate = "2020-01-21";
    public bool stopped = false;
    public GameObject clock;
    public GameObject states;
    public GameObject stopButton;

    public void UpdateAllStates() {
        foreach (Transform child in states.transform) {
            child.gameObject.GetComponent<StatesCases>().CheckDaily(currentDate);
        }

    }

    public void ToggleStopped() {
        stopped = !stopped;
        Text stopButtonText = stopButton.GetComponentInChildren<Text>();
        stopButtonText.text = (stopButtonText.text == "Stop") ? "Resume": "Stop";
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DataLoader>().LoadDataBeforeRendering();
        GameObject.Find("Event").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!stopped) {
            timer -= Time.deltaTime;
            clock.GetComponent<Text>().text = currentDate;
            if (timer <= 0) {
                currentDate = NextDay(currentDate);
                UpdateAllStates();
                timer = timeForOneDay;
            }
        }
    }

    
    
    // Some helper function

    private string NextDay(string today) {
        string[] digits = today.Split ('-');
        int year = int.Parse(digits[0]), month = int.Parse(digits[1]), date = int.Parse(digits[2]);
        // get year of tomorrow
        int y = year, m = month, d = date+1;
        if (month == 12 && date == 31) y = year+1;
        // get month and date of tomorrow
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) {
            if (date == 31) {
                d = 1;
                m = (month == 12) ? 1 : month+1; 
            } 
        } else if (month == 4 || month == 6 || month == 9 || month == 11) {
            if (date == 31) {
                d = 1;
                m = month+1;
            } 
        } else {    // Feb
            if (DateTime.IsLeapYear(year)) {
                if (date == 29) {
                    d = 1;
                    m = 3;
                } 
            } else {
                if (date == 28) {
                    d = 1;
                    m = 3;
                } 
            }
        }
        return y.ToString("D4") + "-" + m.ToString("D2") + "-" + d.ToString("D2");
    }
}
