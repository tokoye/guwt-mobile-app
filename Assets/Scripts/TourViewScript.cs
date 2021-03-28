using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TourViewScript : MonoBehaviour
{
    static public SingleTour tourData;

    string url = "";
    public double lat = 47.66685234815894;      //initial latitude and longitude to center on
    public double lon = -117.40292486555248;
    LocationInfo li;
    public int zoom = 19;
    public int mapWidth = 640;
    public int mapHeight = 650;
    public enum mapType { roadmap, satellite, hybrid, terrain};
    public mapType mapSelected;
    public int scale;
    public string key = "";
    public string[] markers = new string[] {};
    public bool resetMap = false;
    public int currentStop = 0;

    private bool loadingMap = false;

    private IEnumerator mapCoroutine;
    private IEnumerator locationCoroutine;

    public Button resetButton;
    public Button previousStopButton;
    public Button nextStopButton;
    public Button goToStopButton;
    public Text stopInfo;
    public Text tourInfo;

    //this handles getting the google map and placing it on the texture
    IEnumerator GetGoogleMap(double lat, double lon)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
            "&maptype=" + mapSelected + "&key=" + key;

        for (int i = 0; i < tourData.stops.Count; i++)
        {
            url += "&markers=color:red|label:" + i + "|" + tourData.stops[i].lat + "," + tourData.stops[i].lng;
        }

        url += "&markers=color:blue|label:Y|" + lat + "," + lon;

        loadingMap = true;
        //UnityWebRequest www = new UnityWebRequest(url);
        WWW www = new WWW(url);
        yield return (www);
        loadingMap = false;
        GameObject.Find("RawImage").GetComponent<RawImage>().texture = www.texture;
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
            if(lat != 0 && lon != 0)
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
        print("this is a tour" + tourData.name);

         print("Distance: " + DistanceTo(lat, lon, tourData.stops[0].lat, tourData.stops[0].lng));
        //debugTest.text = "worked";


        goToStopButton = GameObject.Find("GoToStopButton").GetComponent<Button>();


        LoadStopInfo();
        LoadTourInfo();

        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);

        resetButton = GameObject.Find("RefreshButton").GetComponent<Button>();
        resetButton.onClick.AddListener(onRefreshButtonClicked);

        previousStopButton = GameObject.Find("PreviousStopButton").GetComponent<Button>();
        previousStopButton.onClick.AddListener(OnPreviousStopButtonClicked);

        nextStopButton = GameObject.Find("NextStopButton").GetComponent<Button>();
        nextStopButton.onClick.AddListener(OnNextStopButtonClicked);

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
            LoadStopInfo();
            resetMap = false;
        }

        ////locationCoroutine = GetLocation();
        ////StartCoroutine(locationCoroutine);
        


    }

    public void LoadTourInfo()
    {
        tourInfo = GameObject.Find("TourDetailsText").GetComponent<Text>();
        string text = tourData.name;
        tourInfo.text = text;
    }

    public void LoadStopInfo()
    {
        stopInfo = GameObject.Find("StopDetailsText").GetComponent<Text>();
        string text = tourData.stops[currentStop].stop_name;
        stopInfo.text = text;

        double distance = DistanceTo(lat, lon, tourData.stops[currentStop].lat, tourData.stops[currentStop].lng);
        if(distance > 10)
        {
            goToStopButton.GetComponentInChildren<Text>().text = distance + " feet";
            goToStopButton.interactable = false;
        }
        else
        {
            goToStopButton.GetComponentInChildren<Text>().text = "go to stop";
            goToStopButton.interactable = true;
        }

    }

    public void onRefreshButtonClicked()
    {
        resetMap = true;
    }

    //decrements to the previous stop
    public void OnPreviousStopButtonClicked()
    {
        if(currentStop > 0)
        {
            currentStop--;
            string text = tourData.stops[currentStop].stop_name;
            stopInfo.text = text;
        }
    }

    //increments to the next stop
    public void OnNextStopButtonClicked()
    {
        if(currentStop < tourData.stops.Count - 1)
        {
            currentStop++;
            string text = tourData.stops[currentStop].stop_name;
            stopInfo.text = text;
        }
    }

    public static double DistanceTo(double lat1, double lon1, string lt2, string ln2)
    {
        //double lat1 = 0;
        //double lon1 = 0;
        double lat2 = 0;
        double lon2 = 0;
        //double.TryParse(lt1, lat1);
        double.TryParse(lt2, out lat2);
        //double.TryParse(ln1, lon1);
        double.TryParse(ln2, out lon2);

        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        return dist * 5280;
    }
}
