using System;

[Serializable]
public class Event
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public string time;
    public string title;
    public string text;

    public string choice1;
    public string target1;
    public double influence1;
    
    public string choice2;
    public string choice3;
}