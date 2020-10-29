using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This namespace is required for using any UI components
using UnityEngine.UI;
//  This namespace is required to use the SceneManager
using UnityEngine.SceneManagement;

public class OrganizationDropdown : MonoBehaviour
{
    public Dropdown drop;
    // Start is called before the first frame update
    void Start()
    {
        
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
