using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataLoader : MonoBehaviour
{

    // Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
    public class DailyCase {
        public int newCase;
        public int death;
		//Assignment constructor.
		public DailyCase (int c, int d)
		{
			newCase = c;
			death = d;
		}
    }


    public TextAsset csvFile; // Reference of CSV file

    Dictionary<string, string> fips2Alpha = new Dictionary<string, string> () {
        {"01" , "AL" },
        {"02" , "AK" },
        {"04" , "AZ" },
        {"05" , "AR" },
        {"06" , "CA" },
        {"08" , "CO" },
        {"09" , "CT" },
        {"10" , "DE" },
        {"11" , "DC" },
        {"12" , "FL" },
        {"13" , "GA" },
        {"15" , "HI" },
        {"16" , "ID" },
        {"17" , "IL" },
        {"18" , "IN" },
        {"19" , "IA" },
        {"20" , "KS" },
        {"21" , "KY" },
        {"22" , "LA" },
        {"23" , "ME" },
        {"24" , "MD" },
        {"25" , "MA" },
        {"26" , "MI" },
        {"27" , "MN" },
        {"28" , "MS" },
        {"29" , "MO" },
        {"30" , "MT" },
        {"31" , "NE" },
        {"32" , "NV" },
        {"33" , "NH" },
        {"34" , "NJ" },
        {"35" , "NM" },
        {"36" , "NY" },
        {"37" , "NC" },
        {"38" , "ND" },
        {"39" , "OH" },
        {"40" , "OK" },
        {"41" , "OR" },
        {"42" , "PA" },
        {"44" , "RI" },
        {"45" , "SC" },
        {"46" , "SD" },
        {"47" , "TN" },
        {"48" , "TX" },
        {"49" , "UT" },
        {"50" , "VT" },
        {"51" , "VA" },
        {"53" , "WA" },
        {"54" , "WV" },
        {"55" , "WI" },
        {"56" , "WY" },
        {"60" , "AS" },
        {"66" , "GU" },
        {"69" , "MP" },
        {"72" , "PR" },
        {"78" , "VI" }
    };
    Dictionary<string, Dictionary<string, DailyCase>> stateDaily = new Dictionary<string, Dictionary<string, DailyCase>>();

    public void LoadDataBeforeRendering()
    {
        string[] records = csvFile.text.Split ('\n');
        foreach (string record in records.Skip(1).ToArray()) {
            string[] fields = record.Split(',');
            // map[date][state AlphaCode] -> DailyCase
            if (!stateDaily.ContainsKey(fields[0])) stateDaily[fields[0]] = new Dictionary<string, DailyCase>();
            string alphaCode = FipsToAlpha(fields[2]);
            stateDaily[fields[0]][alphaCode] = new DailyCase(int.Parse(fields[3]), int.Parse(fields[4]));
        }
    }

    public bool HasNewCases(string date, string state) {
        return stateDaily.ContainsKey(date) && stateDaily[date].ContainsKey(state);
    }

    public int GetDailyCases(string date, string state) {
        return stateDaily[date][state].newCase;
    }

    public int GetDailyDeath(string date, string state) {
        return stateDaily[date][state].death;
    }

    private string FipsToAlpha(string fips) {
        if (!fips2Alpha.ContainsKey(fips)) {
            Debug.Log("Need fips:" + fips + " Total l:" + fips2Alpha.Count);
        }
        return fips2Alpha[fips];
    }




}
