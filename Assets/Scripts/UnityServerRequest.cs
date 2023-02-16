using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityServerRequest : MonoBehaviour
{
    public void SendWebData(string json)
    {
        var request = UnityWebRequest.Post("http://localhost:3000/unity", json);
        request.SetRequestHeader("content-type", "application/json");
        request.uploadHandler.contentType = "application/json";
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

        request.SendWebRequest();
    }
}
