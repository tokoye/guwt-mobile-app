using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TourInspectScript : MonoBehaviour
{

    string url = "";
    public double lat = 47.66685234815894;      //initial latitude and longitude to center on
    public double lon = -117.40292486555248;
    LocationInfo li;
    public int zoom = 19;
    public int mapWidth = 640;
    public int mapHeight = 650;
    public enum mapType { roadmap, satellite, hybrid, terrain };
    public mapType mapSelected;
    public int scale;
    public string key = "";
    public string[] markers = new string[] { };
    public bool resetMap = false;
    public int currentStop = 0;

    private bool loadingMap = false;

    private IEnumerator mapCoroutine;
    private IEnumerator locationCoroutine;

    public Text tourInfo;


    //this handles getting the google map and placing it on the texture
    IEnumerator GetGoogleMap(double lat, double lon)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
            "&maptype=" + mapSelected + "&key=" + key;

        for (int i = 0; i < TourViewScript.tourData.stops.Count; i++)
        {
            url += "&markers=color:red|label:" + i + "|" + TourViewScript.tourData.stops[i].lat + "," + TourViewScript.tourData.stops[i].lng;
        }

        url += "&markers=color:blue|label:Y|" + lat + "," + lon;

        loadingMap = true;
        //UnityWebRequest www = new UnityWebRequest(url);
        WWW www = new WWW(url);
        yield return (www);
        loadingMap = false;
        GameObject.Find("RawImageMap").GetComponent<RawImage>().texture = www.texture;
        StopCoroutine(mapCoroutine);
    }

    //this handles getting the user location
    IEnumerator GetLocation()
    {
        //print("entering");
        //debugTest.text = "Entering";
        //if (!Input.location.isEnabledByUser)
        //    yield break;
        //debugTest.text = "Passed";

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {

            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            if (lat != 0 && lon != 0)
            {
                resetMap = true;
            }
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        Input.location.Stop();
    }

    void RepeatEverySecond()
    {
        locationCoroutine = GetLocation();
        StartCoroutine(locationCoroutine);
    }


    // Start is called before the first frame update
    void Start()
    {

        LoadTourInfo();

        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);

        locationCoroutine = GetLocation();
        StartCoroutine(locationCoroutine);

        InvokeRepeating("RepeatEverySecond", 1, 1);

        resetMap = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (resetMap)
        {
            mapCoroutine = GetGoogleMap(lat, lon);
            StartCoroutine(mapCoroutine);
            resetMap = false;
        }

        ////locationCoroutine = GetLocation();
        ////StartCoroutine(locationCoroutine);



    }

    public void LoadTourInfo()
    {
        tourInfo = GameObject.Find("TourDetailsText").GetComponent<Text>();
        string text = TourViewScript.tourData.name;
        tourInfo.text = text;
    }

}
