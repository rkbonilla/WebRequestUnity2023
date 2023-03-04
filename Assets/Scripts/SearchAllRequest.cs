using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using UnityEngine.UIElements;
using System;
using Newtonsoft.Json;

[Serializable]
public class SearchAllRequest : MonoBehaviour
{
    public TextMeshProUGUI scrollView;
    
    IEnumerator GetRequest(string uri)
    {
        using UnityWebRequest getRequest = UnityWebRequest.Get(uri);
        {
            yield return getRequest.SendWebRequest();

            var newData = Encoding.UTF8.GetString(getRequest.downloadHandler.data);
            Players players = JsonConvert.DeserializeObject<Players>(newData);

            foreach(var entry in players.Entries)
            {
                scrollView.text +=
                    "Username: " + entry.screenName +
                    "\nFirst Name: " + entry.firstName +
                    "\nLast Name: " + entry.lastName +
                    "\nStart Date: " + entry.startDate +
                    "\nScore: " + entry.score +
                    "\n\n";
            }
        }
    }
    
    private void Start()
    {
        StartCoroutine(GetRequest("http://localhost:3000/getAllUnityEntries"));
    }
}
