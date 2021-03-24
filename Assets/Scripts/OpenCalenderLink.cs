using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCalenderLink : MonoBehaviour
{
    public void OpenChannel()
    {
        Application.OpenURL("https://www.gonzaga.edu/academics/academic-calendar-resources/academic-calendar/2020-2021-calendar");
    }
}