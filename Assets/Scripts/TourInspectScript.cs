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
    private IEnumerator imageCoroutine;

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

        text = "";
        text += TourViewScript.tourData.description;
        text += "\n\nThere are ";
        text += TourViewScript.tourData.stops.Count;
        text += " stops within this tour\n\nThis tour is ";
        double distance = 0;
        for (int i = 1; i < TourViewScript.tourData.stops.Count; i++)
        {
            distance += DistanceTo(TourViewScript.tourData.stops[i - 1].lat, TourViewScript.tourData.stops[i - 1].lng, TourViewScript.tourData.stops[i].lat, TourViewScript.tourData.stops[i].lng);
        }
        text += Math.Round(distance, 1);
        text += " feet\n\nWould you like to proced to the stops within the tour?";
        GameObject.Find("DescriptionText").GetComponent<Text>().text = text;

        //gets the first picture available and places it
        bool notFound = true;
        int position = -1;
        for (int i = 0; notFound && i < TourViewScript.tourData.stops.Count; i++)
        {
            if (TourViewScript.tourData.stops[i].media.Count > 0)
            {
                position = i;
                notFound = false;
            }
        }
        if(position != -1)
        {
            imageCoroutine = SetImage(position);
            StartCoroutine(imageCoroutine);
        }
    }

    IEnumerator SetImage(int position)
    {
        WWW www = new WWW(TourViewScript.tourData.stops[position].media[0].s3_loc);
        yield return (www);
        GameObject.Find("RawImage").GetComponent<RawImage>().texture = www.texture;
    }

    public static double DistanceTo(string lt1, string ln1, string lt2, string ln2)
    {
        double lat1 = 0;
        double lon1 = 0;
        double lat2 = 0;
        double lon2 = 0;
        double.TryParse(lt1, out lat1);
        double.TryParse(lt2, out lat2);
        double.TryParse(ln1, out lon1);
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
