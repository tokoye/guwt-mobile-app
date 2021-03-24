using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleTour
{
    public string name;
    public string _id;
    public string organization;
    public string departement;
    public string number_of_stops;
    public string createdAt;
    public string updatedAt;
    public string __v;
    public string enabled;
    public List<StopData> stops;
}
