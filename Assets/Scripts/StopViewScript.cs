using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopViewScript : MonoBehaviour
{

    public StopData stopData;
    public int imageIndex = 0;
    private IEnumerator imageCoroutine;

    public Button previousImageButton;
    public Button nextImageButton;

    IEnumerator SetStopImage()
    {
        if (imageIndex < stopData.media.Count)
        {
            print(stopData.media[imageIndex].s3_loc);
            WWW www = new WWW(stopData.media[imageIndex].s3_loc);
            yield return (www);
            GameObject.Find("StopImage").GetComponent<RawImage>().texture = www.texture;


        }

    }
       

    // Start is called before the first frame update
    void Start()
    {
        stopData = TourViewScript.tourData.stops[TourViewScript.currentStop];
        print(stopData.stop_name);
        for (int i = 0; i < stopData.media.Count; i++)
        {
            print(stopData.media[i].s3_loc);

        }

        imageCoroutine = SetStopImage();
        StartCoroutine(imageCoroutine);

        previousImageButton = GameObject.Find("PreviousPicture").GetComponent<Button>();
        previousImageButton.onClick.AddListener(OnPreviousImageButtonClicked);

        nextImageButton = GameObject.Find("NextPicture").GetComponent<Button>();
        nextImageButton.onClick.AddListener(OnNextImageButtonClicked);

        GameObject.Find("DescriptionText").GetComponent<Text>().text = stopData.stop_desc;
        GameObject.Find("MediaCreditsText").GetComponent<Text>().text = stopData.media_desc;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPreviousImageButtonClicked()
    {
        if(imageIndex > 0)
        {
            imageIndex -= 1;
            imageCoroutine = SetStopImage();
            StartCoroutine(imageCoroutine);
        } 
    }

    void OnNextImageButtonClicked()
    {
        if(imageIndex < stopData.media.Count - 1)
        {
            imageIndex += 1;
            imageCoroutine = SetStopImage();
            StartCoroutine(imageCoroutine);
        }
    }
}
