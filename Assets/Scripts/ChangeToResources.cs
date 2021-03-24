using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeToResources : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadResourceScene()
    {
        SceneManager.LoadScene("GU Resources");
    }
}
