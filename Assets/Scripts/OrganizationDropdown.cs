using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This namespace is required for using any UI components
using UnityEngine.UI;
//  This namespace is required to use the SceneManager
using UnityEngine.SceneManagement;

public class OrganizationDropdown : MonoBehaviour
{
    List<string> stringOptions = new List<string> { "Select Organization", "GU's History Department","GU's Environmental Science Department", "Spokane Parks & Rec", "Spokane River"};
    List<Dropdown.OptionData> optionDataOptions = new List<Dropdown.OptionData>();
    public Dropdown drop;
    public Dropdown.OptionData fillData;
    public Button beginTourButton;
   
    // Start is called before the first frame update
    void Start()
    {
        //connecting the dropdown and button to the UI elements
        drop = GameObject.Find("OrganizationDropdown").GetComponent<Dropdown>();
        beginTourButton = GameObject.Find("beginTourButton").GetComponent<Button>();
        drop.ClearOptions();

        //adding every option in the stringOptions list. Note: must become Dropdown.OptionData items first. 
        foreach(string i in stringOptions)
        {
            fillData = new Dropdown.OptionData();
            fillData.text = i;
            optionDataOptions.Add(fillData);
            drop.options.Add(fillData);
        }

    }

    List<string> getOrganizations()
    {
        //TODO: connect to aws and get the list of organizations with tours nearby 
        return new List<string> { };
    }

    void dropdownClicked(Dropdown item)
    {

    }
}
