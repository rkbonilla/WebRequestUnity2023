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

            var newData = System.Text.Encoding.UTF8.GetString(getRequest.downloadHandler.data);
            var newGetRequestData = JsonUtility.FromJson<PlayerData>(newData);

            screenNameGet.text = "Username: " + newGetRequestData.screenName;
            firstName.text = "First Name: " + newGetRequestData.firstName;
            lastName.text = "Last Name: " + newGetRequestData.lastName;
            dateStarted.text = "Start Date: " + newGetRequestData.startDate;
            score.text = "Score: " + newGetRequestData.score;
        }
    }

    //IEnumerator SendWebData(string json)
    //{
    //    using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/searchUnityEntries", json))
    //    {
    //        request.SetRequestHeader("content-type", "application/json");
    //        request.uploadHandler.contentType = "application/json";
    //        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

    //        yield return request.SendWebRequest();

    //        if (request.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(request.error);
    //        }
    //        else
    //        {
    //            Debug.Log("DataObj Posted");
    //        }


    //        request.uploadHandler.Dispose();
    //        request.downloadHandler.Dispose();
    //    }
    //}

    public void SendButton()
    {
        var screenName = screenNameSend.text;
        //PlayerData formData = new PlayerData();
        //formData.screenName = screenNameSend.text;
        //formData.firstName = "1";
        //formData.lastName = "1";
        //formData.startDate = "1";
        //formData.score = int.Parse("1");

        Debug.Log(screenName);
        StartCoroutine(GetRequest("http://localhost:3000/searchUnityEntries?screenName="+screenName));
    }
}
