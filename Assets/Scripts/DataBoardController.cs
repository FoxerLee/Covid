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

        public AdvisorChar(string n, string d, string pos, string neg) {
            name = n;
            data = d;
            positive = pos;
            negative = neg;
        }
    }



    public int hmIdx = 0;         // Character index
    public int caIdx = 0;

    public Sprite[] caImgArr;
    public Sprite[] hmImgArr;

    public GameObject healthMinister;
    public GameObject covidAdvisor;


    private AdvisorChar[] caArr = { 
        new AdvisorChar("Dr. White", "          81\n\n       	  Immunology\n\n             Very High\n", " + 10% Vaccine Dev", "\n\n - 5% Propoganda"), 
        new AdvisorChar("Dr. Black", "          66\n\n       	  Pneumology\n\n             High\n", " + 10% Propoganda\n\n + 10% Economy", "\n\n\n\n - 5% Vaccine Dev")
    };

    private AdvisorChar[] hmArr = {
        new AdvisorChar("Mr. Roy", "             54\n\n             High", " + 20% Propoganda\n\n + 20% Economy", "\n\n\n\n - 15% Cooperation"), 
        new AdvisorChar("Mr. Buzz", "             51\n\n             High", " + 15% Vaccine Dev\n\n + 10% Economy", "\n\n\n\n - 5% Satisfaction")
    };




    // Logic about CovidAdvisor on the right part

    public void HideRightBoard() {
        covidAdvisor.transform.Find("DataBoard").gameObject.SetActive(false);
    }

    public void ShowRightBoard() {
        covidAdvisor.transform.Find("DataBoard").gameObject.SetActive(true);
    }
    
    public void SwitchRightCharacter(int value) {
        caIdx = value;
        // Change Text and Img
        GameObject board = covidAdvisor.transform.Find("DataBoard").gameObject ;
        AdvisorChar ch = caArr[caIdx];
        board.transform.Find("Name").GetComponent<Text>(). text = ch.name;
        board.transform.Find("Data").GetComponent<Text>(). text = ch.data;
        board.transform.Find("Positive").GetComponent<Text>(). text = ch.positive;
        board.transform.Find("Negative").GetComponent<Text>(). text = ch.negative;
        covidAdvisor.GetComponent<Image>().sprite = caImgArr[caIdx];
    } 

    // Logic about HealthMinister on the left part

    public void HideLeftBoard() {
        healthMinister.transform.Find("DataBoard").gameObject.SetActive(false);
    }

    public void ShowLeftBoard() {
        healthMinister.transform.Find("DataBoard").gameObject.SetActive(true);
    }

    public void SwitchLeftCharacter(int value) {
        hmIdx = value;
        // Change Text and Img
        GameObject board = healthMinister.transform.Find("DataBoard").gameObject ;
        AdvisorChar ch = hmArr[hmIdx];
        board.transform.Find("Name").GetComponent<Text>(). text = ch.name;
        board.transform.Find("Data").GetComponent<Text>(). text = ch.data;
        board.transform.Find("Positive").GetComponent<Text>(). text = ch.positive;
        board.transform.Find("Negative").GetComponent<Text>(). text = ch.negative;
        healthMinister.GetComponent<Image>().sprite = hmImgArr[hmIdx];
    } 

    void Start() {
        HideRightBoard();
        HideLeftBoard();
    }

}
