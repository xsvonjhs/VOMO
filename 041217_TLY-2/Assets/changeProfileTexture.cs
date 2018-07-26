using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeProfileTexture : MonoBehaviour {

    string PHPRead = "";
    private string DisplayID;
    private string PHPFeedback;
    string UrlProPic;

    public GameObject GO_MainProfilePic;
    RawImage Raw_MainProfilePic;

    public Texture2D DefaultProfilePic;
    string TestUrl = "http://builtbee.com/VOMO/uploadProfilePic_Copy.php";




    // Use this for initialization
    void Start() {
       // Raw_MainProfilePic.texture = DefaultProfilePic;
        DisplayID = (PlayerPrefs.GetString("userID"));
       // DisplayID = "32";
        Raw_MainProfilePic =  (RawImage) GO_MainProfilePic.GetComponent<RawImage>();
        PHPRead = "http://builtbee.com/VOMO/uploads/profilepic_info/IDPic" + DisplayID + ".txt";
        StartCoroutine(SendIDtoPHP(DisplayID));



    }

    // Update is called once per frame
    void Update() {

    }




    IEnumerator SendIDtoPHP(string MYID)
    {
        WWWForm IDForm = new WWWForm();

        IDForm.AddField("ProPicID", MYID);

        WWW sendIDtofile = new WWW(TestUrl, IDForm);

        yield return sendIDtofile;      
        PHPFeedback = sendIDtofile.text;
        Debug.Log(PHPFeedback);
        UrlProPic = "http://builtbee.com/VOMO/uploads/" + PHPFeedback;

        if (PHPFeedback == null || PHPFeedback == "" || PHPFeedback == "Unable to open file!")
        {
            Raw_MainProfilePic.texture = DefaultProfilePic;
        }

        else
        {
            StartCoroutine(ChangeProfilePicture(UrlProPic));
        }

    }


    IEnumerator ChangeProfilePicture( string url)
    {
        WWW www = new WWW(url);
        yield return www;
        Raw_MainProfilePic.texture = www.texture;
    }


}
