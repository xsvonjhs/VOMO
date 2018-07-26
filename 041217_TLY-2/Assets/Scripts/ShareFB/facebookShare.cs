using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public class facebookShare : MonoBehaviour
{
    public GameObject BtnShare;
    public GameObject BtnVideo;
    public GameObject BtnUpload;
    public string message;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * if (GetComponent<FunctionScript>().uploadvid == null) //dont know why unable to function
        {
            BtnShare.SetActive(false);
        }
        else
        {
            BtnShare.SetActive(true);
        }
        */
    }

    private bool isProcessing = false;
    private bool isFocus = false;

    public void ShareBtnPress()
    {
        if (!isProcessing)
        {
            //string VideoPath = Application.persistentDataPath + "/" + "giphy.mp4";
            //Debug.Log(VideoPath);
            StartCoroutine(Share());
        }
    }

    IEnumerator Share()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        //string destination = Path.Combine(Application.persistentDataPath, "giphy.mp4");
        //string destination = Application.persistentDataPath + "/" + "giphy.mp4";
        string destination = GetComponent<FunctionScript>().uploadvid;
        Debug.Log(destination);

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + message);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "video/mp4");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your VOMO Picture/Video");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void clickVideo()
    {
        BtnShare.SetActive(true);
        BtnUpload.SetActive(false);
        BtnVideo.SetActive(true);
    }

    public void clickShare()
    {
        BtnShare.SetActive(false);
        BtnUpload.SetActive(true);
        BtnVideo.SetActive(true);
    }
}
