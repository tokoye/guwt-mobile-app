﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RequestOrgs : MonoBehaviour
{
    private string orgUrl = "https://backend.gonzagatours.app/api/organizations";
    private string toursUrl = "https://backend.gonzagatours.app/tour/tours";
    private string APIKey = "Api-Key f38b7720-eb36-4f40-992e-d4ee85d24b04";
    public List<string> orgNames = new List<string>();
    public List<Button> buttonList = new List<Button>();
    public List<Dropdown.OptionData> orgDropdownNames = new List<Dropdown.OptionData>();
    List<string> allTourNames = new List<string>();
    public UnityWebRequest orgRequest;
    public UnityWebRequest toursRequest;
    public Dropdown drop;
    public List<Dropdown.OptionData> listOptions;
    public int categorySelected;
    public TourData tourResponse;
    public TourData orgTourData; //tour data specific to the organization selected
    public List<string> organizationOfEachTour = new List<string>();

    //Buttons that are appear as each optional tour 
    public Button firstTourButton;
    public Button secondTourButton;
    public Button thirdTourButton;
    public Button fourthTourButton;
    public Button fifthTourButton;
    public Button sixthTourButton;
    public Button seventhTourButton;
    public Button eighthTourButton;
    public Button ninthTourButton;
    public Button tenthTourButton;

    public Button beginTourButton;
    public Button inspectTourButton;


    public void Start()
    {
        FindGameObjects();
        StartCoroutine(GetOrganizations());
        StartCoroutine(GetTours());

        //adding listener to dropdown, which responds appropriately when a selection is made
        drop.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(drop);
        });


        beginTourButton = GameObject.Find("beginTourButton").GetComponent<Button>();
        inspectTourButton = GameObject.Find("inspectTourButton (1)").GetComponent<Button>();

        beginTourButton.interactable = false;
        inspectTourButton.interactable = false;

    }

    //This function just locates the game objects on the screen and connects them to their respective
    //names here, so that they can be changed and altered dynamically. 
    public void FindGameObjects()
    {
        drop = GameObject.Find("OrganizationDropdown").GetComponent<Dropdown>();
        drop.ClearOptions();

        firstTourButton = GameObject.Find("TourListItem1").GetComponent<Button>();
        buttonList.Add(firstTourButton);
        secondTourButton = GameObject.Find("TourListItem2").GetComponent<Button>();
        buttonList.Add(secondTourButton);
        thirdTourButton = GameObject.Find("TourListItem3").GetComponent<Button>();
        buttonList.Add(thirdTourButton);
        fourthTourButton = GameObject.Find("TourListItem4").GetComponent<Button>();
        buttonList.Add(fourthTourButton);
        fifthTourButton = GameObject.Find("TourListItem5").GetComponent<Button>();
        buttonList.Add(fifthTourButton);
        sixthTourButton = GameObject.Find("TourListItem6").GetComponent<Button>();
        buttonList.Add(sixthTourButton);
        seventhTourButton = GameObject.Find("TourListItem7").GetComponent<Button>();
        buttonList.Add(seventhTourButton);
        eighthTourButton = GameObject.Find("TourListItem8").GetComponent<Button>();
        buttonList.Add(eighthTourButton);
        ninthTourButton = GameObject.Find("TourListItem9").GetComponent<Button>();
        buttonList.Add(ninthTourButton);
        tenthTourButton = GameObject.Find("TourListItem10").GetComponent<Button>();
        buttonList.Add(tenthTourButton);

        firstTourButton.onClick.AddListener(() => OnTourButtonClicked(0));
        secondTourButton.onClick.AddListener(() => OnTourButtonClicked(1));
        thirdTourButton.onClick.AddListener(() => OnTourButtonClicked(2));
        fourthTourButton.onClick.AddListener(() => OnTourButtonClicked(3));
        fifthTourButton.onClick.AddListener(() => OnTourButtonClicked(4));
        sixthTourButton.onClick.AddListener(() => OnTourButtonClicked(5));
        seventhTourButton.onClick.AddListener(() => OnTourButtonClicked(6));
        eighthTourButton.onClick.AddListener(() => OnTourButtonClicked(7));
        ninthTourButton.onClick.AddListener(() => OnTourButtonClicked(8));
        tenthTourButton.onClick.AddListener(() => OnTourButtonClicked(9));

    }

    void OnTourButtonClicked(int index)
    {
        Debug.Log("Tour Button " + index + " Pressed:" + orgTourData.data[index].name);
        TourViewScript.tourData = orgTourData.data[index];

        beginTourButton.interactable = true;
        inspectTourButton.interactable = true;
    }

    //This function makes a call to the database and populates the Organization dropdown with those organizations.
    public IEnumerator GetOrganizations()
    {
        orgRequest = UnityWebRequest.Get(orgUrl);
        orgRequest.SetRequestHeader("Authentication", APIKey);

        yield return orgRequest.SendWebRequest();

        if (orgRequest.result == UnityWebRequest.Result.ConnectionError || orgRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(orgRequest.error);
        }
        else
        {
            // Show database results as text
            Debug.Log("ORGANIZATION RESULT:    " + orgRequest.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<OrgData>(orgRequest.downloadHandler.text);


            List<string> stringOrgNames = new List<string>();

            stringOrgNames.Add("Select an Organization");
            
            //read all organization names to a list
            for(int i = 0; i < response.data.Count; i++)
            {
                stringOrgNames.Add(response.data[i].name);
            }

            //populate the dropdown menu options 
            foreach (string s in stringOrgNames)
            {
                Dropdown.OptionData fillData = new Dropdown.OptionData();
                fillData.text = s;
                drop.options.Add(fillData);
                drop.RefreshShownValue();
            }
        }
    }

    //This function calls the Tours API, which returns critical data about tour stops, media, location, etc. 
    public IEnumerator GetTours()
    {
        toursRequest = UnityWebRequest.Get(toursUrl);
        toursRequest.SetRequestHeader("Authentication", APIKey);

        yield return toursRequest.SendWebRequest();

        //If the database call fails for any reason, log the failure. Otherwise, carry on with JSON data.
        if (toursRequest.result == UnityWebRequest.Result.ConnectionError || toursRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(toursRequest.error);
        }
        else
        {
            // Show database results as text
            Debug.Log("TOURS RESULT:    " + toursRequest.downloadHandler.text);
            tourResponse = JsonConvert.DeserializeObject<TourData>(toursRequest.downloadHandler.text);

            //adding tour names and tour organizations to their own lists
            for(int i = 0; i < tourResponse.data.Count; i++)
            {
                //doing a check to make sure that the tour has been enabled and there are stops within the tour
                if (tourResponse.data[i].enabled && tourResponse.data[i].stops.Count > 0)
                {
                    allTourNames.Add(tourResponse.data[i].name);
                    organizationOfEachTour.Add(tourResponse.data[i].organization);
                }
                else
                {
                    tourResponse.data.RemoveAt(i);
                    i--;
                }
            }
        }
    }


    //This function recognizes and responds to a selection that is made from the dropdown menu. 
    void DropdownValueChanged(Dropdown change)
    {
        string organizationSelectedString = change.options[change.value].text;
        categorySelected = change.value;
        enableButtons(organizationSelectedString);
    }

    //It enables the appropriate number of buttons when a category is selected depending on how many tours said category offers.
    //It also changes the text of the buttons to 
    private void enableButtons(string orgSelectedString)
    {
        disableButtons();
        int count = 0;
        orgTourData.data.Clear();
        for (int i = 0; i < allTourNames.Count; i++)
        {
            //If the organization of the tour equals the name of the organization selected, enable button and label it.
            if (organizationOfEachTour[i].Equals(orgSelectedString))
            {
                orgTourData.data.Add(tourResponse.data[i]);
                buttonList[count].interactable = true;
                buttonList[count].GetComponentInChildren<Text>().text = allTourNames[i];
                count++;
            }

        }
    }

    private void disableButtons()
    {
        for(int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].interactable = false;
            buttonList[i].GetComponentInChildren<Text>().text = "-";
        }
    }
}