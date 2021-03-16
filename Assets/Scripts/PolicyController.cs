using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyController : MonoBehaviour
{
    public static PolicyController instance = null;

    public float vac = 0f;
    public float vacCost = 10f;
    public float vacPer = 0.1f;

    public float pro = 0f;
    public float proCost = 10f;
    public float proPer = 0.1f;

    public float coo = 0f;
    public float cooCost = 10f;
    public float cooPer = 0.1f;

    GameObject vacBar;
    GameObject proBar;
    GameObject cooBar;

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

        BindPolicyUI();
    }
    

    public void UpdatePolicy(string policy)
    {
        if (policy == "vac" && vac < 10f && vacCost < GameController.instance.currentMoney)
        {
            vac += 1f;
            GameController.instance.currentMoney -= vacCost;
            vacCost += (1 + vacPer) * vacCost;

            GameController.instance.casePer += -0.01f;
            GameController.instance.moneyPer += -0.01f;
        }
        else if (policy == "pro" && pro < 10f && proCost < GameController.instance.currentMoney)
        {
            pro += 1f;
            GameController.instance.currentMoney -= proCost;
            proCost += (1 + proPer) * proCost;

            GameController.instance.casePer += -0.01f;
            GameController.instance.moneyPer += -0.01f;
        }
        else if (policy == "coo" && coo < 10f && cooCost < GameController.instance.currentMoney)
        {
            coo += 1f;
            GameController.instance.currentMoney -= cooCost;
            cooCost += (1 + cooPer) * cooCost;

            GameController.instance.casePer += -0.01f;
            GameController.instance.moneyPer += -0.01f;
        }

        UpdatePolicyBar();
    }

    private void UpdatePolicyBar()
    {
        RectTransform vacBarRectTransform = vacBar.GetComponent<RectTransform>();
        float preVac = vac / 10.0f * 214.0f;
        vacBarRectTransform.sizeDelta = new Vector2(preVac, 21f);

        RectTransform proBarRectTransform = proBar.GetComponent<RectTransform>();
        float prePro = pro / 10.0f * 214.0f;
        proBarRectTransform.sizeDelta = new Vector2(prePro, 21f);

        RectTransform cooBarRectTransform = cooBar.GetComponent<RectTransform>();
        float preCoo = coo / 10.0f * 214.0f;
        cooBarRectTransform.sizeDelta = new Vector2(preCoo, 21f);

    }

    private void BindPolicyUI()
    {
        vacBar = GameObject.Find("Vaccine/VacBar");
        proBar = GameObject.Find("Propaganda/ProBar");
        cooBar = GameObject.Find("Cooperation/CooBar");

    }

    public void Reset() {
        vac = 0f;
        vacCost = 10f;
        vacPer = 0.1f;

        pro = 0f;
        proCost = 10f;
        proPer = 0.1f;

        coo = 0f;
        cooCost = 10f;
        cooPer = 0.1f;

        UpdatePolicyBar();
    }
}
