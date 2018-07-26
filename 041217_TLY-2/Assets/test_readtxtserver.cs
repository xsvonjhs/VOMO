using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
 


public class test_readtxtserver : MonoBehaviour {

 
    string PHPIDUrl = "http://www.pictalive.com/upload1210_ID.php";
    string PHP_ProfilePic = "http://www.pictalive.com/uploadProfilePic.php";

    private string DisplayID;


    public void profilePicUpdate()
    {
        DisplayID = (PlayerPrefs.GetString("userID"));
        StartCoroutine(SendIDtoPHP(DisplayID));

        using (var client = new WebClient())
        {
          //  client.UploadFile(PHPimgUrl, "POST", uploadimg);
        }

    }



    IEnumerator SendIDtoPHP(string MYID)
    {


        WWWForm IDForm = new WWWForm();

        IDForm.AddField("playerID", DisplayID);

        WWW sendIDtofile = new WWW(PHPIDUrl, IDForm);

        yield return sendIDtofile;
       // Feedbackerror = sendIDtofile.text;


    }

}
