using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToResources : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadARscene()
    {
        SceneManager.LoadScene("GU Resources");
    }
}
