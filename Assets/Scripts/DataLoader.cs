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



    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, Dictionary<string, DailyCase>> stateDaily = new Dictionary<string, Dictionary<string, DailyCase>>();
        string[] records = csvFile.text.Split ('\n');
        foreach (string record in records.Skip(1).ToArray()) {
            string[] fields = record.Split(',');
            // map[state][date] -> DailyCase
            if (!stateDaily.ContainsKey(fields[1])) stateDaily[fields[1]] = new Dictionary<string, DailyCase>();
            stateDaily[fields[1]][fields[0]] = new DailyCase(int.Parse(fields[3]), int.Parse(fields[4]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
