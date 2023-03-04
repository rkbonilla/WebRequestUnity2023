using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string screenName;
    public string firstName;
    public string lastName;
    public string startDate;
    public int score;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
}

public class Players
{
    public PlayerData[] Entries { get; set; }
}
