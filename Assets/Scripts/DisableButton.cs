using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
public class beginTourButton : MonoBehaviour
{
    public Button tourButton;

    // Start is called before the first frame update
    void Start()
    {
        tourButton = GameObject.Find("beginTourButton").GetComponent<Button>();
        tourButton.interactable = false;
    }

    public void changeButton(bool tourSelected)
    {
        if (tourSelected == true)
        {
            tourButton.interactable = true;
        }
        else
        {
            tourButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
