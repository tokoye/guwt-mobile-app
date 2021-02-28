using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
 
public class RequestOrgs : MonoBehaviour
{
    private string url = "https://backend.gonzagatours.app/api/organizations";
    Text testText;
    public Button DatabaseButton;
   
    
    public void generateRequest()
    {
        DatabaseButton = GameObject.Find("DatabaseButton").GetComponent<Button>();
        StartCoroutine(GetText());
    }

    public IEnumerator GetText()
    {
        UnityWebRequest request= UnityWebRequest.Get(url);
        request.SetRequestHeader("Authentication", "");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);

            var response = JsonConvert.DeserializeObject<List<RequestOrgs>>(request.downloadHandler.text);
            Debug.Log(response[1].name);

            // Or retrieve results as binary data
            //byte[] results = request.downloadHandler.data;
        }
    }
}