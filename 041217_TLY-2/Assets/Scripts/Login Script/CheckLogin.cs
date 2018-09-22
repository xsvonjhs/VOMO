using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CheckLogin : MonoBehaviour
{
    public InputField input;
    public string notice;

    public string check_username;
    public string check_password;

    public GameObject loggin_panel;
    public Text loggin_warntext = null;
    public GameObject Back_button;
    public GameObject Next_button;

    private string[] respond_code;
    public string send_ID;
    public string FrameCode;

    string LoginCheckURL = AccessData.Server+"/login.php";
    string PHPFrameUrl = AccessData.Server+"/returnFramelist.php";

    // Use this for initialization
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerinfo.dat");
            PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
            file.Close();


            PlayerPrefs.SetString("userID", data.username);
            PlayerPrefs.SetString("Frame", data.FrameCode);
            Debug.Log("Logged in");
            SceneManager.LoadScene("2.0_front");
        }
    }

    public void CheckUser()
    {
        
        input = GameObject.Find("In_Username").GetComponent<InputField>();
        check_username = input.text;
        PlayerPrefs.SetString("username", check_username);

        input = GameObject.Find("In_Password").GetComponent<InputField>();
        check_password = input.text;

        StartCoroutine(FeedbackFromPHP(check_username, check_password));

    }

    IEnumerator FeedbackFromPHP(string user_username, string user_password)
    {
        WWWForm form = new WWWForm();

        form.AddField("check_usernamePost", check_username);
        form.AddField("check_passwordPost", check_password);

        WWW www = new WWW(LoginCheckURL, form);

        yield return www;
        string respond = www.text;
        respond_code = respond.Split(';');

        if (respond_code[0] == "200") // 200 = login successful
        {
            send_ID = respond_code[1];
            yield return StartCoroutine(ReturnFramePHP(respond_code[1]));
            PlayerPrefs.SetString("userID", respond_code[1]);
            Debug.Log(FrameCode);
            PlayerPrefs.SetString("Frame", FrameCode);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/playerinfo.dat");

            PlayerData data = new PlayerData();
            data.username = check_username;
            data.password = check_password;
            data.FrameCode = FrameCode;

            binaryFormatter.Serialize(file, data);
            file.Close();
            SceneManager.LoadScene("2.0_front");
            //loggin_panel.SetActive(true);
            //loggin_warntext.text = "Login Successful! \n Press Next to continue...";
            //Back_button.SetActive(false);
            //Next_button.SetActive(true);

        }

        else if (respond_code[0] == "1139") //1139 = wrong password
        {
            loggin_panel.SetActive(true);
            loggin_warntext.text = "Wrong password or username. \n Please try again.";
            Next_button.SetActive(false);
            Back_button.SetActive(true);
        }

        else if (respond_code[0] == "404") // 404 = no username
        {
            loggin_panel.SetActive(true);
            loggin_warntext.text = "Wrong password or username. \n Please try again.";
            Next_button.SetActive(false);
            Back_button.SetActive(true);
        }
        else if (respond_code[0] == "0000")
        {
            loggin_panel.SetActive(true);
            loggin_warntext.text = "Account inactive. \n Please check your email to activate account.";
            Next_button.SetActive(false);
            Back_button.SetActive(true);
        }

    }

    IEnumerator ReturnFramePHP(string MYID)
    {
        WWWForm IDForm = new WWWForm();

        IDForm.AddField("ID_frame", MYID);
        WWW sendIDtofile = new WWW(PHPFrameUrl, IDForm);

        yield return sendIDtofile;

        FrameCode = sendIDtofile.text;
    }
}

[Serializable]
class PlayerData
{
    public string username;
    public string password;
    public string FrameCode;
}

