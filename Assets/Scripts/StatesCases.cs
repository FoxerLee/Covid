using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random; 

public class StatesCases : MonoBehaviour
{

    public int totalCases = 0;
    public int totalDeath = 0;
    public int dailyCases = 0;
    public int dailyDeaths = 0;
    
    public GameObject bubble;

    private Image img; 
    private Text stateName;
    private DataLoader loader;

    
    public void AddDaily(int cases, int death) {
        // update data
        totalCases += cases;
        totalDeath += death;
        dailyCases = cases;
        dailyDeaths = death;

        UpdateStateColor();

        Invoke("SetupPopup", Random.Range(0f, 0.5f));

    }

    public void CheckDaily(string date) {
        if (loader.HasNewCases(date, name)) {
            int cases = loader.GetDailyCases(date, name);
            int death = loader.GetDailyDeath(date, name);
            AddDaily(cases, death);
        }
    }

    public void Reset() {
        totalCases = 0;
        totalDeath = 0;
        dailyCases = 0;
        dailyDeaths = 0;
        img.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        stateName = GetComponentInChildren<Text>();
        stateName.text = name;
        stateName.fontSize = 50;
        loader = GameObject.Find("GameController").GetComponent<DataLoader>();
        bubble = (GameObject)Resources.Load("Prefabs/Bubble", typeof(GameObject));
    }

    private void UpdateStateColor() {
        // update color
        float percentage = (float) (Math.Log(totalCases) / Math.Log(20000000));
        if (percentage < 0.5f) {
            percentage *= 2f;
            byte g = (byte) ((1-percentage) * 255);
            img.color = new Color32(255, g, g, 255);
        } else {
            percentage = (percentage >= 1.0f) ? 1.0f : 2 * (percentage - 0.5f);
            byte r = (byte) ((1-percentage) * 255);
            img.color = new Color32(r, 0, 0, 255);
        }
    }

    private void SetupPopup() {
        
        GameObject popup = Instantiate (bubble, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.identity);

        if (dailyCases > 0) 
            popup.transform.Find("Cases").GetComponent<Text>().text = "+" + dailyCases;
        if (dailyDeaths > 0)
            popup.transform.Find("Death").GetComponent<Text>().text = "+" + dailyDeaths;
        popup.transform.SetParent(GameObject.Find("Map").transform);

        Destroy(popup, 2.0f);

    }
}
