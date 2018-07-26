using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Vuforia;

public class ScreenShotAR : MonoBehaviour {

    private AndroidUltimatePluginController androidUltimatePluginController;

    public string changeTheScene;

    private bool flashLightSet;

    public GameObject BtnFlashOn;
    public GameObject BtnFlashOff;

    Camera mainCamera;
    RenderTexture renderTex;
    Texture2D screenshot;
    Texture2D LoadScreenshot;
    int width = Screen.width;   // for Taking Picture
    int height = Screen.height; // for Taking Picture
    string fileName;
    string screenShotName = "VOMOar_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png";

    void Start()
    {
        androidUltimatePluginController = AndroidUltimatePluginController.GetInstance();
        bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        bool flashLightSet = CameraDevice.Instance.SetFlashTorchMode(false);
    }

    public void Snapshot()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen()
    {
        yield return new WaitForEndOfFrame();

        /*
        string myFilename = "VOMO.png";
        string myDefaultLocation = Application.persistentDataPath + "/" + myFilename;
        string myFolderLocation = "/storage/emulated/0/DCIM/Camera/";
        string myScreenshotLocation = myFolderLocation + myFilename;

        if (!System.IO.Directory.Exists(myFolderLocation))
        {
            System.IO.Directory.CreateDirectory(myFolderLocation);
        }
        */

        yield return null; // Wait till the last possible moment before screen rendering to hide the UI

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame(); // Wait for screen rendering to complete
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            mainCamera = Camera.main.GetComponent<Camera>(); // for Taking Picture
            renderTex = new RenderTexture(width, height, 24);
            mainCamera.targetTexture = renderTex;
            RenderTexture.active = renderTex;
            mainCamera.Render();
            screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply(); //false
            RenderTexture.active = null;
            mainCamera.targetTexture = null;
        }
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            mainCamera = Camera.main.GetComponent<Camera>(); // for Taking Picture
            renderTex = new RenderTexture(height, width, 24);
            mainCamera.targetTexture = renderTex;
            RenderTexture.active = renderTex;
            mainCamera.Render();
            screenshot = new Texture2D(height, width, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, height, width), 0, 0);
            screenshot.Apply(); //false
            RenderTexture.active = null;
            mainCamera.targetTexture = null;
        }
        // on Win7 - C:/Users/Username/AppData/LocalLow/CompanyName/GameName
        // on Android - /Data/Data/com.companyname.gamename/Files
        //File.WriteAllBytes(Application.persistentDataPath + "/" + screenShotName, screenshot.EncodeToPNG());
        //File.WriteAllBytes(myFilename, screenshot.EncodeToPNG());
        //File.WriteAllBytes("/storage/emulated/0/DCIM/Camera/" + screenShotName, screenshot.EncodeToPNG());
        //File.WriteAllBytes("/Internal storage/DCIM/Camera/" + screenShotName, screenshot.EncodeToPNG()); //totally cannot
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true; // Show UI after we're done

        /*
        System.IO.File.Move(myDefaultLocation, myScreenshotLocation);
        */
        
        yield return new WaitForEndOfFrame();
        /*
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + Application.persistentDataPath + "/" + screenShotName) });
        objActivity.Call("sendBroadcast", objIntent);

        yield return new WaitForEndOfFrame();
        */

        SceneManager.LoadScene(changeTheScene);
    }

    
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


}