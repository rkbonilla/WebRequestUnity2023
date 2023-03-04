using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text;

public class SearchOneEdit : MonoBehaviour
{
    public TMP_InputField screenNameSend, screenNameGet, firstName, lastName, dateStarted, score;

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

                screenNameGet.text = newGetRequestData.screenName;
                firstName.text = newGetRequestData.firstName;
                lastName.text = newGetRequestData.lastName;
                dateStarted.text = newGetRequestData.startDate;
                score.text = newGetRequestData.score.ToString();
            }
        }
    }

    public void SearchButton()
    {
        var screenName = screenNameSend.text;

        //Debug.Log(screenName);
        StartCoroutine(GetRequest("http://localhost:3000/searchUnityEntries?screenName=" + screenName));
    }
    
    IEnumerator SendWebData(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/updateUnityEntry", json))
        {
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler.contentType = "application/json";
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("DataObj Posted");
            }
            request.uploadHandler.Dispose();
            request.downloadHandler.Dispose();
        }
    }

    public void UpdateButton()
    {
        var scoreData = int.Parse(score.text);

        PlayerData formData = new PlayerData();
        formData.screenName = screenNameGet.text;
        formData.firstName = firstName.text;
        formData.lastName = lastName.text;
        formData.startDate = dateStarted.text;
        formData.score = scoreData;

        string json = JsonUtility.ToJson(formData);
        StartCoroutine(SendWebData(json));
    }
}
