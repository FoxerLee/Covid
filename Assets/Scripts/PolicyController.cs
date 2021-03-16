using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyController : MonoBehaviour
{
    public static PolicyController instance = null;
    public float vac = 0f;
    public float pro = 0f;
    public float coo = 0f;

    GameObject vacBar;
    GameObject proBar;
    GameObject cooBar;

    void Awake()
    {
        BindPolicyUI();
    }
    

    public void UpdatePolicy(string policy)
    {
        if (policy == "vac" && vac < 10f)
        {
            vac += 1f;
        }
        else if (policy == "pro" && pro < 10f)
        {
            pro += 1f;
        }
        else if (policy == "coo" && coo < 10f)
        {
            coo += 1f;
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
}
