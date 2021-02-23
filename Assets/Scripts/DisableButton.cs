using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DisableButton : MonoBehaviour
{
    public Button beginTourButton;

    // Start is called before the first frame update
    void Start()
    {
        beginTourButton.GetComponent<Button>();
        beginTourButton.interactable = false;
    }

    public void changeButton(bool tourSelected)
    {
        if (tourSelected == true)
        {
            beginTourButton.interactable = true;
        }
        else
        {
            beginTourButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
