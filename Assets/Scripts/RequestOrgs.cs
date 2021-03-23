using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RequestOrgs : MonoBehaviour
{
    private string orgUrl = "https://backend.gonzagatours.app/api/organizations";
    private string toursUrl = "https://backend.gonzagatours.app/tour/tours";
    private string APIKey = "Api-Key 6d924d5a-cfba-41cc-b21c-6aeabe874a86";
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
        Debug.Log("Tour Button " + index + " Pressed:" + tourResponse.data[index].name);
        TourViewScript.tourData = tourResponse.data[index];
        buttonList.Add(tenthTourButton);
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
            stringOrgNames.Add(response.data[0].organization);
            stringOrgNames.Add(response.data[1].organization);
            stringOrgNames.Add(response.data[2].organization);
            stringOrgNames.Add(response.data[3].organization);
            Debug.Log("STRING NAMES: " + response.data[0].organization + " " + response.data[1].organization);
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

            Debug.Log(tourResponse.data[1].name);
            //allTourNames.Add(tourResponse.data[0].name);
            //allTourNames.Add(tourResponse.data[1].name);
            //setTourButtons();
            //***********
            //WHEN WE DESERIALIZE, THE DEPARTMENT IS NOT POPULATING IN THE SINGLEORG CLASS
            //***********

            //TODO: ADD ALL TOURS FROM DATABASE RESPONSE
            allTourNames.Add(tourResponse.data[0].name);
            organizationOfEachTour.Add(tourResponse.data[0].organization);
            Debug.Log("Name of the first org: " + tourResponse.data[0].organization);
            allTourNames.Add(tourResponse.data[1].name);
            organizationOfEachTour.Add(tourResponse.data[1].organization);
            //setTourButtons();
        }
    }


    //This function recognizes and responds to a selection that is made from the dropdown menu. 
    void DropdownValueChanged(Dropdown change)
    {
        string organizationSelectedString = change.options[change.value].text;
        //Debug.Log("Organization Selected: " + organizationSelectedString);
        categorySelected = change.value;
        setTourButtons(organizationSelectedString);
    }

    //This function accepts a list of strings, which are the names of each tour. It takes those strings
    //and sets the appropriate number of buttons to clickable and sets their title to the name of the tour.
    public void setTourButtons(string orgSelectedString)
    {
        //categorySelected (1=History, 2=CS, 3=Housing)
        int totalTours = 0;
        foreach(string s in allTourNames)
        {
            totalTours++;
        }
        //enabling the appropriate number of tours. 
        //TODO: FIGURE OUT HOW MANY TOURS ARE IN THE SELECTED CATEGORY AND PASS THAT NUMBER, NOT THE TOTAL NUMBER TOURS
        enableButtons(totalTours, orgSelectedString);
    }


    //It enables the appropriate number of buttons when a category is selected depending on how many tours said category offers.
    //It also changes the text of the buttons to 
    private void enableButtons(int num, string orgSelectedString)
    {
        //Debug.Log("Org 1: " + organizationOfEachTour[0] + " Org 2: " + organizationOfEachTour[1]);
        for (int i = 0; i < num; i++)
        {
            buttonList[i].interactable = true;
            //If the organization of the tour equals the name of the organization selected, enable button and label it.
            if (organizationOfEachTour[i].Equals(orgSelectedString))
            {
                buttonList[i].GetComponentInChildren<Text>().text = allTourNames[i];
            }

        }
    }
}