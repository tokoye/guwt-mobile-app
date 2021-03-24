using System.Collections;
using System.Collections.Generic;
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

    private bool loadingMap = false;

    private IEnumerator mapCoroutine;
    private IEnumerator locationCoroutine;

    public Button resetButton;
    public Text debugTest;
    public Text tourInfo;
    public Text stopInfo;

    //this handles getting the google map and placing it on the texture
    IEnumerator GetGoogleMap(double lat, double lon)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
            "&maptype=" + mapSelected + "&key=" + key;

        for (int i = 0; i < markers.Length; i++)
        {
            url += "&markers=color:red|label:S|" + markers[i];
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
            debugTest.text = "Timed Out";
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            debugTest.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            debugTest.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude;
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            resetMap = true;
        }
        Input.location.Stop(); 
    }


    // Start is called before the first frame update
    void Start()
    {
        print("this is a tour" + tourData.name);
        debugTest = GameObject.Find("DebugTest").GetComponent<Text>();
        //debugTest.text = "worked";

        LoadTourInfo();
        LoadStopInfo();

        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);

        resetButton = GameObject.Find("RefreshButton").GetComponent<Button>();
        resetButton.onClick.AddListener(onRefreshButtonClicked);


        locationCoroutine = GetLocation();
        StartCoroutine(locationCoroutine);
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

    }

    public void LoadTourInfo()
    {
        tourInfo = GameObject.Find("TourDetailsText").GetComponent<Text>();
        string text = "";
        text += "Tour Name: " + tourData.name + "\n";
        text += "Organization: " + tourData.organization + "\n";
        text += "Departement: " + tourData.departement + "\n";
        text += "Number of stops: " + tourData.stops.Count + "\n";
        tourInfo.text = text;
    }

    public void LoadStopInfo()
    {
        stopInfo = GameObject.Find("StopDetailsText").GetComponent<Text>();
        string text = "";
        for(int i = 0; i < tourData.stops.Count; i++)
        {
            int temp = i + 1;
            text +=  temp + ") Name: " + tourData.stops[i].stop_name + "\n";
            text += "\t" + "Latitude: " + tourData.stops[i].lat + "\n";
        }
        stopInfo.text = text;
    }

    public void onRefreshButtonClicked()
    {
        resetMap = true;
    }
}
