/* 
*   NatCam Professional
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Examples {

    using UnityEngine;
    using Core;
    using Core.UI;
    using System.Collections;
    using UnityEngine.SceneManagement;
    using System.Net;

#if NATCAM_EXTENDED && NATCAM_PROFESSIONAL
    using Extended;
    using System.Net;
#endif

    public class ReplayCam : NatCamBehaviour {

        public bool a = false;
        float timeRemaining = 15;

        //public bool b = false;

        public string changeTheScene;

        public GameObject Box;
        private bool isProcessing = false;
        private bool isFocus = false;
        public string message;

        public string uploadvid = null;
        string Feedbackerror = null;
        string PHPUrl = "http://www.builtbee.com/VOMO/uploadphp/upload0710_";
        string PHPIDUrl = "http://www.builtbee.com/VOMO/upload1210_ID.php";
        string PHPFrameUrl = "http://www.builtbee.com/VOMO/reg_used_frame.php";

        //ID and Frame
        private string DisplayID;
        private int FrameNum;
        private string FrameNumStr;
        private string UsedFrameNum;
        string FeedbackFramePHP;

        [Header("ReplayCam")]
        public RecordingIcon recordingIcon;
        public bool saveToGallery = false, playVideo = true;

        [Header("Preview")]
        public NatCamPreview previewPanel;

        public override void OnStart () {
            //NatCam.();
            // Display and scale the preview
            previewPanel.Apply(NatCam.Preview);
            

        }

#if NATCAM_EXTENDED && NATCAM_PROFESSIONAL

        void Update () {

            if (a == true)
            {
                NatCam.StartRecording(OnVideoSave);
                timeRemaining -= Time.deltaTime;
                if(timeRemaining <= 0) { NatCam.StopRecording(); a = false; }
            }
            else { NatCam.StopRecording(); }

            if (recordingIcon) recordingIcon.IsRecording = NatCam.IsRecording;
		}

		public void OnVideoSave (SaveMode mode, string path) {
            // Log
            Debug.Log("Recorded video to path: "+path);
            
            // Save to gallery
            if (saveToGallery == true)
            {
                if (saveToGallery && NatCam.Implementation.SaveVideo(path)) Debug.Log("Saved video to gallery");
                saveToGallery = false;
                uploadvid = path;
            }
            #if !UNITYEDITOR && (UNITY_IOS || UNITY_ANDROID)
            
            // Play video
            //if (b == true)
            //{
                //if (playVideo) Handheld.PlayFullScreenMovie(path, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
            if (playVideo) Handheld.PlayFullScreenMovie(path, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);

            StartCoroutine(BoxActivate());
            //Box.SetActive(true);
            //}
            #endif
        }
#endif

        IEnumerator BoxActivate()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            Box.SetActive(true);
        }

        public void abc()
        {
            if (a == false) { a = true;}
            else {a = false;}
        }

        public void changeScene_rec()//int changeTheScene)
        {
            //previewPanel = null;
            NatCam.Release();
            SceneManager.LoadScene(changeTheScene); 
        }

        #region --share--
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
            string destination = uploadvid; // GetComponent<FunctionScript>().uploadvid;
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
                AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your new score");
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

        #endregion

        #region --uploading--
        public void UPLOADVIDtoSERVER()
        {
            DisplayID = (PlayerPrefs.GetString("userID"));
            FrameNum = (PlayerPrefs.GetInt("FrameNumber"));
            FrameNumStr = FrameNum.ToString();
            using (var client = new WebClient())
            {
                client.UploadFile((PHPUrl + DisplayID + "_Frame" + FrameNumStr + ".php"), "POST", uploadvid);
            }

            StartCoroutine(UpdateUsedFramePHP(FrameNum));
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

        #endregion

        /*
        void OnGUI()
        {
            if (timeRemaining > 0)
            {
                GUI.Label(new Rect(200, 200, 400, 200), "Time Remaining: " + timeRemaining);
            }
            else
            {
                GUI.Label(new Rect(200, 200, 200, 200), "Time is up");
                timeRemaining = 15;
            }
        }
        */

    }
}