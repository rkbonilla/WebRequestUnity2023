using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text;

public class InsertRequest : MonoBehaviour
{
    public TMP_InputField screenName, firstName, lastName, dateStarted, score;

    IEnumerator SendWebData(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/saveUnityEntry", json))
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

    public void SendButton()
    {
        var scoreData = int.Parse(score.text);

        PlayerData formData = new PlayerData();
        formData.screenName = screenName.text;
        formData.firstName = firstName.text;
        formData.lastName = lastName.text;
        formData.startDate = dateStarted.text;
        formData.score = scoreData;

        string json = JsonUtility.ToJson(formData);
        StartCoroutine(SendWebData(json));
    }
}
