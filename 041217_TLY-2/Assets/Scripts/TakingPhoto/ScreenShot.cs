using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net;
using Vuforia;

public class ScreenShot : MonoBehaviour
{
    //public string uploadimage;

    private bool isProcessing = false;
    public string changeTheScene;
    public GameObject Box_1;
    public GameObject Box_2;
    public GameObject Loading;

    // for 5.1 PIC_choose
    public GameObject imgConfirm;
    public GameObject imgUpload;
    public GameObject BtnFrameExit;
    public GameObject BtnFrameEnter;

    // for 3.1 screenshotAR
    public GameObject ImgCamera; 
    public GameObject FrameBtn;

    //From FacebookShare.cs
    private bool isProcessingShare = false;
    private bool isFocus = false;
    public string message;
    public GameObject BtnShare;
    //public GameObject ImgShare;

    //from Function script
    public GameObject ImageDis;
    RawImage rawdisimg;

    public GameObject BtnFlashOn;
    public GameObject BtnFlashOff;
    public bool flashLightSet;

    //from Function Script
    public Texture2D containImage = null;
    string Feedbackerror = null;
    string uploadimg = null;
    
    private string DisplayID;
    string PHPimgUrl = "http://www.builtbee.com/VOMO/uploadimgPHP/uploadimgonly_";
    string PHPIDUrl = "http://www.builtbee.com/VOMO/upload1210_ID.php";

    // To recode frame code and frame number
    private int FrameNum;
    private string FrameNumStr;

    private void Start()
    {
        Box_1.SetActive(false);
        Box_2.SetActive(true);
        Loading.SetActive(false);

        // for 5.1 PIC_choose
        imgUpload.SetActive(false);

        // for 3.1 screenshotAR
        ImgCamera.SetActive(true);
        FrameBtn.SetActive(true);

        // for share
        BtnShare.SetActive(false);
        //ImgShare.SetActive(false);
    }

    private void Update()
    {
        if (uploadimg != null)
        {
            //Debug.Log("run if");
            rawdisimg = (RawImage)ImageDis.GetComponent<RawImage>();
            rawdisimg.texture = containImage;

        }

        else
        {
            //Debug.Log("run else");
            rawdisimg = (RawImage)ImageDis.GetComponent<RawImage>();
            rawdisimg.texture = null;
        }
    }

    public void Screenshot()
    {
        if (!isProcessing)
        {
            Box_2.SetActive(false);

            // for 5.1 PIC_choose
            imgConfirm.SetActive(false);
            BtnFrameEnter.SetActive(false);
            BtnFrameExit.SetActive(false);
            
            // for 3.1 screenshotAR
            ImgCamera.SetActive(false);
            FrameBtn.SetActive(false);

            StartCoroutine(TakePicture());
        }
    }

    IEnumerator TakePicture()
    {
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        //put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        //apply
        screenTexture.Apply();

        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-Hmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);

        isProcessing = false;

        yield return new WaitForEndOfFrame();

        //saveToGallery = true;
        uploadimg = destination;
        containImage = screenTexture;
        imgUpload.SetActive(true); // only img bcz btnUpload already activate by frame script
        BtnShare.SetActive(true);
        //ImgShare.SetActive(true);
        Box_1.SetActive(true);
        //SceneManager.LoadScene(changeTheScene);
    }

    #region --upload--
    public void UPLOADIMGtoSERVER()
    {
        Loading.SetActive(true);
        DisplayID = (PlayerPrefs.GetString("userID"));
        FrameNum = (PlayerPrefs.GetInt("FrameNumber"));
        FrameNumStr = FrameNum.ToString();

        using (var client = new WebClient())
        {
            client.UploadFile((PHPimgUrl + DisplayID + "_Frame" + FrameNumStr + ".php"), "POST", uploadimg);
        }

        StartCoroutine(Proceed());
    }


    IEnumerator SendIDtoPHP(string MYID)
    {
        WWWForm IDForm = new WWWForm();

        IDForm.AddField("playerID", DisplayID);

        WWW sendIDtofile = new WWW(PHPIDUrl, IDForm);

        yield return sendIDtofile;
        Feedbackerror = sendIDtofile.text;
    }

    IEnumerator Proceed()
    {
        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene(changeTheScene);
    }

    void OnImageLoad(string imgPath, Texture2D tex)
    {
        Debug.Log("Image Location : " + imgPath);
        containImage = tex;
        // GUI.DrawTexture(new Rect(20, 50, Screen.width - 40, Screen.height - 60), containImage, ScaleMode.ScaleToFit, true);
    }
    #endregion

    #region --flashlight--
    public void flashLightOn()
    {
        //bool flashLightSet = CameraDevice.Instance.SetFlashTorchMode(false);

        //if (flashLightSet == CameraDevice.Instance.SetFlashTorchMode(false))
        //{
        flashLightSet = CameraDevice.Instance.SetFlashTorchMode(true);
        BtnFlashOn.SetActive(false);
        BtnFlashOff.SetActive(true);
    }

    public void flashLightOff()
    {
        flashLightSet = CameraDevice.Instance.SetFlashTorchMode(false);
        BtnFlashOn.SetActive(true);
        BtnFlashOff.SetActive(false);
    }
    #endregion

    #region --share--
    public void ShareBtnPress()
    {
        if (!isProcessingShare)
        {
            //string VideoPath = Application.persistentDataPath + "/" + "giphy.mp4";
            //Debug.Log(VideoPath);
            StartCoroutine(Share());
        }
    }

    IEnumerator Share()
    {
        isProcessingShare = true;

        yield return new WaitForEndOfFrame();

        //string destination = GetComponent<FunctionScript>().uploadvid;
        string destinationShare = uploadimg;
        Debug.Log(destinationShare);

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destinationShare);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + message);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your new score");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessingShare = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

#endregion

    public void ShowCameraIcon()
    {
        ImgCamera.SetActive(true);
        Box_2.SetActive(true);
    }
}