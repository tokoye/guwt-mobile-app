using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourViewScript : MonoBehaviour
{
    string url = "";
    public double lat = 47.66685234815894;
    public double lon = -117.40292486555248;
    LocationInfo li;
    public int zoom = 14;
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

    public Button resetButton;

    //this handles getting the google map and placing it on the texture
    IEnumerator GetGoogleMap(double lat, double lon)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
            "&maptype=" + mapSelected + "&key=" + key;

        for (int i = 0; i < markers.Length; i++)
        {
            url += "&markers=color:red|label:G|" + markers[i];
        }

        loadingMap = true;
        //UnityWebRequest www = new UnityWebRequest(url);
        WWW www = new WWW(url);
        yield return (www);
        loadingMap = false;
        GameObject.Find("RawImage").GetComponent<RawImage>().texture = www.texture;
        StopCoroutine(mapCoroutine);

    }


    // Start is called before the first frame update
    void Start()
    {
        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);

        resetButton = GameObject.Find("RefreshButton").GetComponent<Button>();
        resetButton.onClick.AddListener(onRefreshButtonClicked);
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

    public void onRefreshButtonClicked()
    {
        resetMap = true;
    }
}
