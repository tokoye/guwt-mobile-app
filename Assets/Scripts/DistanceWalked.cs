using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceWalked : MonoBehaviour
{
    static int distanceWalked = 0;
    Text distanceWalkedText;

    // Start is called before the first frame update
    void Start()
    {
        distanceWalked = PlayerPrefs.GetInt("distance", 0);
        distanceWalkedText = GameObject.Find("DistanceWalked").GetComponent<Text>();
        distanceWalkedText.text = distanceWalked.ToString() + " Feet Walked";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
