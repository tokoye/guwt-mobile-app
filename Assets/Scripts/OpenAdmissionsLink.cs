using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAdmissionsLink : MonoBehaviour
{
    public void OpenChannel()
    {
        Application.OpenURL("https://www.gonzaga.edu/admission");
    }
}