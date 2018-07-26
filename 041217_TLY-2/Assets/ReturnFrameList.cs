using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ReturnFrameList : MonoBehaviour {

    private string DisplayID;
    private string PHPFrameUrl = AccessData.Server+"/return_framelist.php";
    public string[] FrameArr;
    private string FrameCode;

    //Frame Button, used then deactivate
    public GameObject ButtonFrame1;
    public GameObject ButtonFrame2;
    public GameObject ButtonFrame3;
    public GameObject ButtonFrame4;
    public GameObject ButtonFrame5;
    public GameObject ButtonFrame6;


    // Use this for initialization
    void Start()
    {
        DisplayID = PlayerPrefs.GetString("userID");
        //StartCoroutine(ReturnFramePHP(DisplayID));
        FrameCode = PlayerPrefs.GetString("Frame");
        FrameArr = FrameCode.Split(';');
        //Debug.Log(FrameCode);

        //Debug.Log(FrameArr[0]);
        //Debug.Log(FrameArr[1]);
        //Debug.Log(FrameArr[2]);
        //Debug.Log(FrameArr[3]);
        //Debug.Log(FrameArr[4]);
        //Debug.Log(FrameArr[5]);

        PlayerPrefs.SetString("Frame_1", FrameArr[0]);
        PlayerPrefs.SetString("Frame_2", FrameArr[1]);
        PlayerPrefs.SetString("Frame_3", FrameArr[2]);
        PlayerPrefs.SetString("Frame_4", FrameArr[3]);
        PlayerPrefs.SetString("Frame_5", FrameArr[4]);
        PlayerPrefs.SetString("Frame_6", FrameArr[5]); 
        //PlayerPrefs.SetString("URLIDtgt", FrameCode);

        GameObject SelectFrameGO = GameObject.Find("FrameChoose");
        SelectFrame selectframescript = SelectFrameGO.GetComponent<SelectFrame>();
        GameObject PicChooseGO = GameObject.Find("Script_Manager");
        Pic_choose pic_chooseScript = PicChooseGO.GetComponent<Pic_choose>();

        if (FrameCode == "1;0;0;0;0;0")
        {
            Debug.Log("in 10000!");
            selectframescript.f6.SetActive(true);
            pic_chooseScript.PT_condition = true;
            selectframescript.PT_condition = true;
            PlayerPrefs.SetInt("FrameNumber", 6);
        }
        else
        {
            if (FrameArr[0] == "1")
            {
                ButtonFrame1.SetActive(false);
                Debug.Log("I am here");
            }
            if (FrameArr[1] == "1")
            {
                ButtonFrame2.SetActive(false);
            }
            if (FrameArr[2] == "1")
            {
                ButtonFrame3.SetActive(false);
            }
            if (FrameArr[3] == "1")
            {
                ButtonFrame4.SetActive(false);
            }
            if (FrameArr[4] == "1")
            {
                ButtonFrame5.SetActive(false);
            }
            if (FrameArr[5] == "1")
            {
                ButtonFrame6.SetActive(false);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator ReturnFramePHP(string MYID)
    {



        WWWForm IDForm = new WWWForm();

        IDForm.AddField("ID_frame", DisplayID);

        WWW sendIDtofile = new WWW(PHPFrameUrl, IDForm);

        yield return sendIDtofile;
        FrameCode = sendIDtofile.text;
    }




}
