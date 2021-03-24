using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSignUp : MonoBehaviour
{
    public void OpenChannel()
    {
        Application.OpenURL("https://apply.gonzaga.edu/portal/campusvisit");
    }
}