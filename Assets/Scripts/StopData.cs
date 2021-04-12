using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StopData
{
    public string stop_name;
    public string _id;
    public string stop_desc;
    public int stop_number;
    public string lng;
    public string lat;
    public List<MediaData> media;
    public string media_desc;
}
