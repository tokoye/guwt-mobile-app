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
    public List<string> orgNames = new List<string>();
    public List<Dropdown.OptionData> orgDropdownNames = new List<Dropdown.OptionData>();
    public UnityWebRequest request;
    public Dropdown drop;
    public List<Dropdown.OptionData> listOptions;



    public void Start()
    {
        drop = GameObject.Find("OrganizationDropdown").GetComponent<Dropdown>();
        drop.ClearOptions();
        StartCoroutine(GetText());
    }

    public IEnumerator GetText()
    {
        request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authentication", "Api-Key 6d924d5a-cfba-41cc-b21c-6aeabe874a86");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show database results as text
            Debug.Log(request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<OrgData>(request.downloadHandler.text);


            List<string> stringOrgNames = new List<string>();
            stringOrgNames.Add(response.data[0].department);
            stringOrgNames.Add(response.data[1].department);
            stringOrgNames.Add(response.data[2].department);
            stringOrgNames.Add(response.data[3].department);

            foreach(string s in stringOrgNames)
            {
                Dropdown.OptionData fillData = new Dropdown.OptionData();
                fillData.text = s;
                drop.options.Add(fillData);
                drop.RefreshShownValue();
            }
        }
    }

}