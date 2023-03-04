using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text;

public class SearchOneRequest : MonoBehaviour
{
    public TMP_InputField screenNameSend;
    public TMP_Text screenNameGet, firstName, lastName, dateStarted, score;

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest getRequest = UnityWebRequest.Get(uri))
        {
            yield return getRequest.SendWebRequest();

            if (getRequest.responseCode == 404)
            {
                screenNameGet.text = "Player not found";
                firstName.text = "";
                lastName.text = "";
                dateStarted.text = "";
                score.text = "";

            }
            else
            {
                var newData = System.Text.Encoding.UTF8.GetString(getRequest.downloadHandler.data);
                var newGetRequestData = JsonUtility.FromJson<PlayerData>(newData);

                screenNameGet.text = "Username: " + newGetRequestData.screenName;
                firstName.text = "First Name: " + newGetRequestData.firstName;
                lastName.text = "Last Name: " + newGetRequestData.lastName;
                dateStarted.text = "Start Date: " + newGetRequestData.startDate;
                score.text = "Score: " + newGetRequestData.score;
            }
        }
    }

    public void SendButton()
    {
        var screenName = screenNameSend.text;

        //Debug.Log(screenName);
        StartCoroutine(GetRequest("http://localhost:3000/searchUnityEntries?screenName=" + screenName));
    }
}
