using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;
    public static float timeForOneDay = 1.0f;
    public float timer = timeForOneDay;
    public string currentDate = "2020-01-19";
    public bool stopped = false;
    public GameObject clock;
    public GameObject states;
    public GameObject stopButton;
    public GameObject endingPage;
    public GameObject policyController;

    public TextAsset jsonFile;

    public float currentMoney = 100f;

    public float casePer = 1f;
    public float moneyPer = 1f;

    private int countryCases = 0;
    private int endCases = 300000;
    
    private int countryDeaths = 0;
    
    private float endMoney = 1000f;
    private float dailyMoney = 2f;
    
    
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
    }

    public void ToggleStopped() {
        stopped = !stopped;
        Text stopButtonText = stopButton.GetComponentInChildren<Text>();
        stopButtonText.text = (stopButtonText.text == "Stop") ? "Resume": "Stop";
    }

    // TODO: Add logic about character choices
    public void Reset() {
        stopped = false;
        currentDate = "2020-01-19";
        stopped = false;
        countryCases = 0;
        // endCases = 1000;
        currentMoney = 0f;
        endMoney = 1000f;
        dailyMoney = 2f;

        casePer = 1f;
        moneyPer = 1f;

        ResetAllState();

        GetComponent<DataBoardController>().Reset();

        UpdateMoneyBar();

        policyController.GetComponent<PolicyController>().Reset();

        endingPage.GetComponent<EndingBehavior>().HideEndingPage();
    }


    // Start is called before the first frame update
    void Start()
    {
        EventController.instance.BindEventUI();
        EventController.instance.allEvents = JsonUtility.FromJson<Events>(jsonFile.text);
        // EventController.instance.allEvents = LoadJson.LoadJsonFromFile<Events>();
        GetComponent<DataLoader>().LoadDataBeforeRendering();
        // GameObject.Find("Event").SetActive(false);
        EventController.instance.eventBox.SetActive(false);
        GameObject.Find("Canvas/Ending").GetComponent<EndingBehavior>().HideEndingPage();

        // GameOver("ending1");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!stopped) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                currentDate = NextDay(currentDate);
                UpdateAllStates();
                timer = timeForOneDay;
                
                // event related
                EventController.instance.CheckEvents();
                
                // economy related
                currentMoney += dailyMoney * moneyPer;
                UpdateMoneyBar();

                PolicyController.instance.UpdatePolicyBar();

                // case ending
                if (countryCases >= endCases)
                {
                    GameOver("ending1");
                }
                // money ending
                if (currentMoney <= 0.0f)
                {
                    GameOver("ending2");
                }
                if (currentDate == "2020-04-01")
                {
                    GameOver("ending3");
                }

            }
            clock.GetComponent<Text>().text = currentDate;
        }

        // if (currentDate == "2020-01-25") {
        //     GameOver("ending1");
        // }


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


    private void UpdateCaseBar()
    {
        GameObject theBar = GameObject.Find("Cases/CaseBar");
        RectTransform theBarRectTransform = theBar.GetComponent<RectTransform>();
        float percentCase = (float)countryCases / (float)endCases * 288.0f;
        theBarRectTransform.sizeDelta = new Vector2(percentCase, 31f);

        GameObject theText = GameObject.Find("Cases/Text");
        float percent = (float)countryCases / 1000.0f;
        theText.GetComponent<Text>().text = "Cases: " + percent.ToString("0.000") + "k";
    }


    private void UpdateMoneyBar()
    {
        GameObject theBar = GameObject.Find("Economy/MoneyBar");
        RectTransform theBarRectTransform = theBar.GetComponent<RectTransform>();
        float percentMoney = currentMoney / endMoney * 288.0f;
        theBarRectTransform.sizeDelta = new Vector2(percentMoney, 31f);

        GameObject theText = GameObject.Find("Economy/Text");
        theText.GetComponent<Text>().text = "Economy: " + currentMoney.ToString();
    }


    private void GameOver(string ending) {
        stopped = true;
        if (ending == "ending1")
        {
            endingPage.GetComponent<EndingBehavior>().TriggerEnding(
                "There are too many patients, All hospitals are full, people are rushing to stock up drugs and goods... Everything is out of control...",
                PolicyController.instance.vac / 10f,
                PolicyController.instance.pro / 10f,
                PolicyController.instance.coo / 10f,
                countryCases,
                countryDeaths,
                (int)currentMoney
            );
        }
        else if (ending == "ending2")
        {
            endingPage.GetComponent<EndingBehavior>().TriggerEnding(
                "Although you have contained the epidemic, your treasury has dried up. The stock market crashed and prices soared. Is it all worth it?",
                PolicyController.instance.vac / 10f,
                PolicyController.instance.pro / 10f,
                PolicyController.instance.coo / 10f,
                countryCases,
                countryDeaths,
                (int)currentMoney
            );
        }
        else if (ending == "ending3")
        {
            endingPage.GetComponent<EndingBehavior>().TriggerEnding(
                "You successfully made it through April, but this is just the beginning. More incidents are waiting for you to solve, and more problems need to be dealt with by you. \n\n To be continued...",
                PolicyController.instance.vac / 10f,
                PolicyController.instance.pro / 10f,
                PolicyController.instance.coo / 10f,
                countryCases,
                countryDeaths,
                (int)currentMoney
            );
        }
        
    }

    private void UpdateAllStates() {
        float dailyTotalCases = 0;
        float dailyTotalDeaths = 0;
        foreach (Transform child in states.transform) {

            child.gameObject.GetComponent<StatesCases>().CheckDaily(currentDate);
            // calculate cases based on influence
            dailyTotalCases += child.gameObject.GetComponent<StatesCases>().dailyCases;
            dailyTotalDeaths += child.gameObject.GetComponent<StatesCases>().dailyDeaths;
        }

        Debug.Log(casePer);
        if (casePer < 0f)
        {
            casePer = 0f;
        }
        dailyTotalCases *= casePer;
        dailyTotalDeaths *= casePer;
        countryCases += (int)dailyTotalCases;
        countryDeaths += (int)dailyTotalDeaths;

        // update case bar
        UpdateCaseBar();
    }

    private void ResetAllState() {
        foreach (Transform child in states.transform) {
            child.gameObject.GetComponent<StatesCases>().Reset();
        }

        // update case bar
        UpdateCaseBar();
    }
}
