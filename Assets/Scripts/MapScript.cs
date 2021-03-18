using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
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

    private bool loadingMap = false;
    private IEnumerator mapCoroutine;

    //""https://maps.googleapis.com/maps/api/staticmap?center=47.66685234815894,-117.40292486555248&zoom=14&size=640x640&maptype=roadmap&key=AIzaSyBuTO1TPvcKxBfDqa0RXmhcIjJ32Xr-ZOA"
    //""https://maps.googleapis.com/maps/api/staticmap?center=47.66685234815894,-117.40292486555248&zoom=14&size=640x640&maptype=roadmap&key=AIzaSyBuTO1TPvcKxBfDqa0RXmhcIjJ32Xr-ZOA&markers=color:red|label:G|47.66685234815894,-117.40292486555248"

    IEnumerator GetGoogleMap(double lat, double lon)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight +
            "&maptype=" + mapSelected + "&key=" + key;

        for (int i = 0; i < markers.Length; i++){
            url += "&markers=color:red|label:G|" + markers[i];
        }
        
        loadingMap = true;
        //UnityWebRequest www = new UnityWebRequest(url);
        WWW www = new WWW(url);
        yield return (www);
        loadingMap = false;
        gameObject.GetComponent<RawImage>().texture = www.texture;
        StopCoroutine(mapCoroutine);

    }


    // Start is called before the first frame update
    void Start()
    {
        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);
    }


    // Update is called once per frame
    void Update()
    {
        //mapCoroutine = GetGoogleMap(lat, lon);
        //StartCoroutine(mapCoroutine);

    }
}
