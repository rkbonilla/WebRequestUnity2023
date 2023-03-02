using System;
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
