using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBoardController : MonoBehaviour
{

    private class AdvisorChar {
        public string name;
        public string data;
        public string positive;
        public string negative;
        public float caseFactor;
        public float moneyFactor;

        public AdvisorChar(string n, string d, string pos, string neg, float f1, float f2) {
            name = n;
            data = d;
            positive = pos;
            negative = neg;
            caseFactor = f1;
            moneyFactor = f2;
        }
    }



    public int hmIdx = 0;         // Character index
    public int caIdx = 0;

    public Sprite[] caImgArr;
    public Sprite[] hmImgArr;

    public GameObject healthMinister;
    public GameObject covidAdvisor;


    private AdvisorChar[] caArr = { 
        new AdvisorChar("Dr. White", "          81\n\n       	  Immunology\n\n             Very High\n", " + 10% Case Reduction", "\n\n - 5% Economy", -0.1f, -0.05f), 
        new AdvisorChar("Dr. Black", "          66\n\n       	  Pneumology\n\n             High\n", " + 10% Economy", "\n\n + 5% Case Increase", 0.05f, 0.1f),
        new AdvisorChar("Dr. Bar", "          45\n\n       	  Surgery\n\n             Medium\n", " + 50% Economy", "\n\n + 20% Case Increase", 0.5f, 0.2f),
        new AdvisorChar("Dr. Foo", "          51\n\n       	  Epidemiology\n\n             High\n", " + 20% Case Reduction", "\n\n - 15% Economy", -0.2f, -0.15f)
    };

    private AdvisorChar[] hmArr = {
        new AdvisorChar("Mr. Roy", "             54\n\n             High", " + 20% Economy", "\n\n + 15% Case Increase", 0.15f, 0.2f), 
        new AdvisorChar("Mr. Buzz", "             51\n\n             High", " + 5% Case Reduction\n\n + 1% Economy", "", -0.05f, 0.1f),
        new AdvisorChar("Mr. Luck", "             71\n\n             High", " + 20% Case Reduction", "\n\n - 20% Economy", -0.2f, -0.2f),
        new AdvisorChar("Mr. Scott", "             68\n\n             High", " + 2% Economy", "", 0f, 0.02f)
    };




    // Logic about CovidAdvisor on the right part

    public void HideRightBoard() {
        covidAdvisor.transform.Find("DataBoard").gameObject.SetActive(false);
    }

    public void ShowRightBoard() {
        covidAdvisor.transform.Find("DataBoard").gameObject.SetActive(true);
    }
    
    public void SwitchRightCharacter(int value) {
        // unpack current buff
        GameController.instance.casePer -= caArr[caIdx].caseFactor;
        GameController.instance.moneyPer -= caArr[caIdx].moneyFactor;

        caIdx = value;
        // Change Text and Img
        GameObject board = covidAdvisor.transform.Find("DataBoard").gameObject ;
        AdvisorChar ch = caArr[caIdx];
        board.transform.Find("Name").GetComponent<Text>(). text = ch.name;
        board.transform.Find("Data").GetComponent<Text>(). text = ch.data;
        board.transform.Find("Positive").GetComponent<Text>(). text = ch.positive;
        board.transform.Find("Negative").GetComponent<Text>(). text = ch.negative;
        covidAdvisor.GetComponent<Image>().sprite = caImgArr[caIdx];

        // packing new buff
        GameController.instance.casePer += caArr[caIdx].caseFactor;
        GameController.instance.moneyPer += caArr[caIdx].moneyFactor;
    } 

    // Logic about HealthMinister on the left part

    public void HideLeftBoard() {
        healthMinister.transform.Find("DataBoard").gameObject.SetActive(false);
    }

    public void ShowLeftBoard() {
        healthMinister.transform.Find("DataBoard").gameObject.SetActive(true);
    }

    public void SwitchLeftCharacter(int value) {
        // unpack current buff
        GameController.instance.casePer -= hmArr[hmIdx].caseFactor;
        GameController.instance.moneyPer -= hmArr[hmIdx].moneyFactor;

        hmIdx = value;
        // Change Text and Img
        GameObject board = healthMinister.transform.Find("DataBoard").gameObject ;
        AdvisorChar ch = hmArr[hmIdx];
        board.transform.Find("Name").GetComponent<Text>(). text = ch.name;
        board.transform.Find("Data").GetComponent<Text>(). text = ch.data;
        board.transform.Find("Positive").GetComponent<Text>(). text = ch.positive;
        board.transform.Find("Negative").GetComponent<Text>(). text = ch.negative;
        healthMinister.GetComponent<Image>().sprite = hmImgArr[hmIdx];

        // add new buff
        GameController.instance.casePer += hmArr[hmIdx].caseFactor;
        GameController.instance.moneyPer += hmArr[hmIdx].moneyFactor;
    } 

    public void Reset() {
        // SwitchLeftCharacter(0);
        // SwitchRightCharacter(0);
        GameObject board = healthMinister.transform.Find("DataBoard").gameObject ;
        board.transform.Find("Dropdown").GetComponent<Dropdown>().value = 0;
        board = covidAdvisor.transform.Find("DataBoard").gameObject ;
        board.transform.Find("Dropdown").GetComponent<Dropdown>().value = 0;
    }

    void Start() {
        GameController.instance.casePer += caArr[0].caseFactor;
        GameController.instance.moneyPer += hmArr[0].moneyFactor;
        HideRightBoard();
        HideLeftBoard();
        SwitchLeftCharacter(0);
        SwitchRightCharacter(0);
    }


}
