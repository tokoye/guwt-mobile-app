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
    public Button DatabaseButton;
    public List<string> orgNames = new List<string>();
    public List<Dropdown.OptionData> orgDropdownNames = new List<Dropdown.OptionData>();
    List<string> allTourNames = new List<string>();
    public UnityWebRequest orgRequest;
    public UnityWebRequest toursRequest;
    public Dropdown drop;
    public List<Dropdown.OptionData> listOptions;
    public int categorySelected;

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
        StartCoroutine(GetText());
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
        secondTourButton = GameObject.Find("TourListItem2").GetComponent<Button>();
        thirdTourButton = GameObject.Find("TourListItem3").GetComponent<Button>();
        fourthTourButton = GameObject.Find("TourListItem4").GetComponent<Button>();
        fifthTourButton = GameObject.Find("TourListItem5").GetComponent<Button>();
        sixthTourButton = GameObject.Find("TourListItem6").GetComponent<Button>();
        seventhTourButton = GameObject.Find("TourListItem7").GetComponent<Button>();
        eighthTourButton = GameObject.Find("TourListItem8").GetComponent<Button>();
        ninthTourButton = GameObject.Find("TourListItem9").GetComponent<Button>();
        tenthTourButton = GameObject.Find("TourListItem10").GetComponent<Button>();
    }

    //This function makes a call to the database and populates the Organization dropdown with those organizations.
    public IEnumerator GetText()
    {
<<<<<<< HEAD
        orgRequest = UnityWebRequest.Get(orgUrl);
        orgRequest.SetRequestHeader("Authentication", APIKey);
=======
        request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authentication", "Api-Key 6d924d5a-cfba-41cc-b21c-6aeabe874a86");
>>>>>>> nick

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
            stringOrgNames.Add(response.data[0].department);
            stringOrgNames.Add(response.data[1].department);
            stringOrgNames.Add(response.data[2].department);
            stringOrgNames.Add(response.data[3].department);

            foreach (string s in stringOrgNames)
            {
                Dropdown.OptionData fillData = new Dropdown.OptionData();
                fillData.text = s;
                drop.options.Add(fillData);
                drop.RefreshShownValue();
            }
        }
    }



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
            var response = JsonConvert.DeserializeObject<OrgData>(toursRequest.downloadHandler.text);

            allTourNames.Add(response.data[0].name);
            allTourNames.Add(response.data[1].name);
            setTourButtons();
        }
    }


    //This function recognizes and responds to a selection that is made from the dropdown menu. 
    void DropdownValueChanged(Dropdown change)
    {
        categorySelected = change.value;
        setTourButtons();
    }

    //This function accepts a list of strings, which are the names of each tour. It takes those strings
    //and sets the appropriate number of buttons to clickable and sets their title to the name of the tour.
    public void setTourButtons()
    {
        //categorySelected (1=History, 2=CS, 3=Housing)
        int totalTours = 0;
        foreach(string s in allTourNames)
        {
            totalTours++;
        }
        //enabling the appropriate number of tours
        enableButtons(totalTours);
    }


    //This is absolutely **horrendous code**, but that is just how it is going to be for now. It enables the appropriate number of buttons
    //when a category is selected depending on how many tours said category offers.
    private void enableButtons(int num)
    {
        switch(num)
        {
            case 1:
                firstTourButton.interactable = true;
                break;
            case 2: 
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                break;
            case 3:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                break;
            case 4:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                break;
            case 5:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                break;
            case 6:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                sixthTourButton.interactable = true;
                break;
            case 7:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                sixthTourButton.interactable = true;
                seventhTourButton.interactable = true;
                break;
            case 8:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                sixthTourButton.interactable = true;
                seventhTourButton.interactable = true;
                eighthTourButton.interactable = true;
                break;
            case 9:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                sixthTourButton.interactable = true;
                seventhTourButton.interactable = true;
                eighthTourButton.interactable = true;
                ninthTourButton.interactable = true;
                break;
            case 10:
                firstTourButton.interactable = true;
                secondTourButton.interactable = true;
                thirdTourButton.interactable = true;
                fourthTourButton.interactable = true;
                fifthTourButton.interactable = true;
                sixthTourButton.interactable = true;
                seventhTourButton.interactable = true;
                eighthTourButton.interactable = true;
                ninthTourButton.interactable = true;
                tenthTourButton.interactable = true;
                break;
            default:
                firstTourButton.interactable = false;
                secondTourButton.interactable = false;
                thirdTourButton.interactable = false;
                fourthTourButton.interactable = false;
                fifthTourButton.interactable = false;
                sixthTourButton.interactable = false;
                seventhTourButton.interactable = false;
                eighthTourButton.interactable = false;
                ninthTourButton.interactable = false;
                tenthTourButton.interactable = false;
                break;
        }
    }

}