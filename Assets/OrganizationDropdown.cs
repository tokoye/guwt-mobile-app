using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This namespace is required for using any UI components
using UnityEngine.UI;
//  This namespace is required to use the SceneManager
using UnityEngine.SceneManagement;

public class OrganizationDropdown : MonoBehaviour
{
    List<string> options = new List<string> { "option 1.1", "option 2.2" };
    public Dropdown drop;
    // Start is called before the first frame update
    void Start()
    {
        drop = GetComponent<Dropdown>();
        drop.ClearOptions();
        drop.AddOptions(options);
    }

    List<string> getOrganizations()
    {
        //TODO: connect to aws and get the list of organizations with tours nearby 
        return new List<string> { };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void dropdownClicked()
    {
        SceneManager.LoadScene(1);
    }
}
