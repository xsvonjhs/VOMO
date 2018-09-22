using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ImageVideoContactPicker;

public class FunctionScript : MonoBehaviour
{

    Texture2D       containImage    = null;
    string          log             = "";
    string          uploadimg       = null;
    public string   uploadvid       = null;
    string          Feedbackerror   = null;
    public GameObject Loading;

    //For 5.1_PICchoose
    public GameObject   ImageDis;
    RawImage            rawdisimg;

    // For 2.1_ProfilePic
    public GameObject   GO_profile;
    RawImage            Raw_profile;
    public GameObject   Loadingraw;

    // Scence
    Scene               currentscence;
    string              scencename;

    string PHPUrl           = "http://www.builtbee.com/VOMO/uploadphp/upload0710_";
    string PHPimgUrl        = "http://www.builtbee.com/VOMO/uploadimgPHP/uploadimgonly_";
    string PHPIDUrl         = "http://www.builtbee.com/VOMO/upload1210_ID.php";
    string PHP_ProfilePic   = "http://www.builtbee.com/VOMO/uploadProfilePic.php"; 
    string PHPFrameUrl      = "http://www.builtbee.com/VOMO/reg_used_frame.php";   //uploadProfilePic_Copy.php

    //ID and Frame
    private string DisplayID;
    private int FrameNum;
    private string FrameNumStr;
    private string UsedFrameNum;


    //test
    string testURL;
    string FeedbackFramePHP;

    void Update()
    {
        currentscence = SceneManager.GetActiveScene();
        scencename = currentscence.name;

        if (scencename == "2.1_ProfilePic")                        // Start 2.1_ProfilePic
        {
            if (uploadimg != null)
            { 
                Raw_profile = (RawImage)GO_profile.GetComponent<RawImage>();
                Raw_profile.texture = containImage;
                var tempColor = Raw_profile.color;
                tempColor.a = 1f;
                tempColor.r = 1f;
                tempColor.g = 1f;
                tempColor.b = 1f;
                Raw_profile.color = tempColor;


            }

            else
            {
                Raw_profile = (RawImage)GO_profile.GetComponent<RawImage>();
                Raw_profile.texture = null;
            }


        }                                                          // End 2.1_ProfilePic







        else if (scencename == "5.1 PIC_choose")                    // Start 5.1 PIC_choose
        {
            //Debug.Log("5.1 corrcte");
            if (uploadimg != null)
            {
               // Debug.Log("run if");
                rawdisimg = (RawImage)ImageDis.GetComponent<RawImage>();
                rawdisimg.texture = containImage;

            }

            else
            {
               // Debug.Log("run else");
                rawdisimg = (RawImage)ImageDis.GetComponent<RawImage>();
                rawdisimg.texture = null;
            }
        }                                                              // End5.1 PIC_choose                                                              




        else
        {
            // Just in case anything happended

        }





   }


    void OnEnable()
    {
        PickerEventListener.onImageSelect += OnImageSelect;
        PickerEventListener.onImageLoad += OnImageLoad;
        PickerEventListener.onVideoSelect += OnVideoSelect;
        PickerEventListener.onError += OnError;
        PickerEventListener.onCancel += OnCancel;
  
    }

    void OnDisable()
    {
        PickerEventListener.onImageSelect -= OnImageSelect;
        PickerEventListener.onImageLoad -= OnImageLoad;
        PickerEventListener.onVideoSelect -= OnVideoSelect;
        PickerEventListener.onError -= OnError;
        PickerEventListener.onCancel -= OnCancel;
    }

    void OnImageSelect(string imgPath)
    {
        Debug.Log("Image Location : " + imgPath);
        log += "\nImage Path : " + imgPath;
        uploadimg = imgPath;
  

    }


    void OnImageLoad(string imgPath, Texture2D tex)
    {
        Debug.Log("Image Location : " + imgPath);
        containImage = tex;
        // GUI.DrawTexture(new Rect(20, 50, Screen.width - 40, Screen.height - 60), containImage, ScaleMode.ScaleToFit, true);
      
    }


    void OnVideoSelect(string vidPath)
    {
        Debug.Log("Video Location : " + vidPath);
        log += "\nVideo Path : " + vidPath;
        //  Handheld.PlayFullScreenMovie("file://" + vidPath, Color.blue, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
        uploadvid = vidPath;
        
    }

    void OnError(string errorMsg)
    {
        Debug.Log("Error : " + errorMsg);
        log += "\nError :" + errorMsg;
    }

    void OnCancel()
    {
        Debug.Log("Cancel by user");
        log += "\nCancel by user";
    }


    // Use this for initialization
    void Start()
    {
        Loading.SetActive(false);
    }


    public void acessImage()
    {
       AndroidPicker.BrowseImage();
    }

    public void acessVideo()
    {
       AndroidPicker.BrowseVideo();
    }

    public void UPLOADIMGtoSERVER()
    {
        StartCoroutine(Wait500ms(DisplayID));
        Loading.SetActive(true);
        DisplayID = (PlayerPrefs.GetString("userID"));
        StartCoroutine(SendIDtoPHP(DisplayID));

        using (var client = new WebClient())
        {
            client.UploadFile(PHPimgUrl, "POST", uploadimg);
        }

    }

    public void PicChooseUploadIMG()
    {
        DisplayID = (PlayerPrefs.GetString("userID"));
        FrameNum = (PlayerPrefs.GetInt("FrameNumber"));
        FrameNumStr = FrameNum.ToString();
        using (var client = new WebClient())
        {
            client.UploadFile((PHPimgUrl + DisplayID + "_Frame" + FrameNumStr + ".php"), "POST", uploadimg);
        }


    }

    public void UPLOADVIDtoSERVER()
    {
        Loading.SetActive(true);
        DisplayID = (PlayerPrefs.GetString("userID"));
        FrameNum = (PlayerPrefs.GetInt("FrameNumber"));
        FrameNumStr = FrameNum.ToString();
        using (var client = new WebClient())
        {

            client.UploadFile((PHPUrl + DisplayID + "_Frame" + FrameNumStr + ".php"), "POST", uploadvid);
        }

        StartCoroutine(UpdateUsedFramePHP(FrameNum));



    }

    public void UPLOADProfilePictoSERVER()
    {

        DisplayID = (PlayerPrefs.GetString("userID"));
        StartCoroutine(SendIDtoPHP(DisplayID));
        //Loadingraw.SetActive(true);
        using (var client = new WebClient())
        {
            PlayerPrefs.SetString("URLIDtgt", "RUnning");
            client.UploadFile(PHP_ProfilePic, "POST", uploadimg);
            PlayerPrefs.SetString("URLIDtgt", uploadimg);


        }



    }


    public void UpdateUsedFrame()
    {
        StartCoroutine(UpdateUsedFramePHP(FrameNum));
    }


    public void ChangeToScene(string sceneToChangeTo)
    {

        SceneManager.LoadScene(sceneToChangeTo);
    }


    IEnumerator SendIDtoPHP(string MYID)
    {


        WWWForm IDForm = new WWWForm();

        IDForm.AddField("playerID", DisplayID);

        WWW sendIDtofile = new WWW(PHPIDUrl, IDForm);

        yield return sendIDtofile;
        Feedbackerror = sendIDtofile.text;


    }


    IEnumerator UpdateUsedFramePHP(int MYID)
    {
        string GetFrameNumStr = MYID.ToString();
        WWWForm FrameForm = new WWWForm();

        FrameForm.AddField("IDuser", DisplayID);
        FrameForm.AddField("frameguna", GetFrameNumStr);

        WWW sendFrmtofile = new WWW(PHPFrameUrl, FrameForm);

        yield return sendFrmtofile;
        // Feedbackerror = sendFrmtofile.text;
        FeedbackFramePHP = sendFrmtofile.text;
        PlayerPrefs.SetString("URLIDtgt", FeedbackFramePHP);
    }

    IEnumerator Wait500ms(string aniduse)
    {
        yield return new WaitForSeconds(0.5f);
    }


}
