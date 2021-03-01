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
        request.SetRequestHeader("Authentication", "Api-Key b49aad95-4493-44ff-b18a-be8bb2d1e012");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);

            var response = JsonConvert.DeserializeObject<OrgData>(request.downloadHandler.text);
            Debug.Log(response.data[2].name);

            // Or retrieve results as binary data
            //byte[] results = request.downloadHandler.data;
        }
    }
}