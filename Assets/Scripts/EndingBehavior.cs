using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingBehavior : MonoBehaviour
{
    /*
     * desc : ending description
     * vacP : percentage of vaccine development, eg. 95.0f
     * proP : percentage of propogation
     * cooP : percentage of cooperation 
     * cases : number of total cases
     * death : number of total death
     * economy: number of economy
     */
    public void TriggerEnding(string desc, float vacP, float proP, float cooP, int cases, int death, int economy) {
        gameObject.transform.Find("Description").GetComponent<Text>().text = desc;
        gameObject.transform.Find("CaseNum").GetComponent<Text>().text = "" + cases;
        gameObject.transform.Find("DeathNum").GetComponent<Text>().text = "" + death;
        gameObject.transform.Find("CoinsNum").GetComponent<Text>().text = "" + economy;
        float width = gameObject.transform.Find("Vaccine").GetComponent<RectTransform>().rect.width;
        RectTransform rt;
        rt = gameObject.transform.Find("Vaccine/VacBar").GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (width * 0.01f * vacP, rt.sizeDelta.y);
        rt = gameObject.transform.Find("Propaganda/ProBar").GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (width * 0.01f * proP, rt.sizeDelta.y);
        rt = gameObject.transform.Find("Cooperation/CooBar").GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (width * 0.01f * cooP, rt.sizeDelta.y);

        ShowEndingPage();
    }

    public void Restart() {
        HideEndingPage();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void HideEndingPage() {
        gameObject.SetActive(false);
    }

    public void ShowEndingPage() {
        gameObject.SetActive(true);
    }
}
