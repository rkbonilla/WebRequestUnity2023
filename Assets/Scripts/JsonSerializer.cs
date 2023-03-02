using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using System.Text;

public class JsonSerializer : MonoBehaviour
{
    public TMP_InputField playerName, level, elapsedTime;
    public DataClass dataObj;
    public DataClass newDataObj;
    public string filePath;

    // Start is called before the first frame update
    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, "saveData.txt");
        dataObj = new DataClass();
        dataObj.level = 1;
        dataObj.timeElapsed = 4543.090890f;
        dataObj.name = "Roberto";
        string json = JsonUtility.ToJson(dataObj);
        Debug.Log(json);
        File.WriteAllText(filePath, json);
        //UnityServerRequest newRequest = new UnityServerRequest();
        //newRequest.SendWebData(json);
        StartCoroutine(SendWebData(json));
        StartCoroutine(GetRequest("http://localhost:3000/SendUnityData"));

        newDataObj = JsonUtility.FromJson<DataClass>(json);
        Debug.Log(newDataObj.name);
    }

    IEnumerator SendWebData(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/unity", json))
        {
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler.contentType = "application/json";
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
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

    IEnumerator GetRequest(string uri)
    {
        using(UnityWebRequest getRequest = UnityWebRequest.Get(uri))
        {
            yield return getRequest.SendWebRequest();

            var newData = System.Text.Encoding.UTF8.GetString(getRequest.downloadHandler.data);
            var newGetRequestData = JsonUtility.FromJson<DataClass>(newData);

            Debug.Log(newGetRequestData.name);
            Debug.Log(newGetRequestData.level);
            Debug.Log(newGetRequestData.timeElapsed);
        }
    }

    public void SendButton()
    {
        var levelData = int.Parse(level.text);
        var timeData = float.Parse(elapsedTime.text);
        DataClass formData = new DataClass();
        formData.name = playerName.text;
        formData.level = levelData;
        formData.timeElapsed = timeData;
        string json = JsonUtility.ToJson(formData);
        StartCoroutine(SendWebData(json));
    }
}
