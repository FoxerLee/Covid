
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJson
{
    public static T LoadJsonFromFile<T>()where T:class
    {
        if (!File.Exists(Application.dataPath + "/Data/events.json"))
        {
            Debug.Log(Application.dataPath);
            Debug.LogError("Don't Find");
            return null;
        }
 
        StreamReader sr = new StreamReader(Application.dataPath + "/Data/events.json");
        if (sr == null)
        {
            return null;
        }
        string json = sr.ReadToEnd();
 
        if (json.Length > 0)
        {
            return JsonUtility.FromJson<T>(json);
        }
        return null;
    }
}