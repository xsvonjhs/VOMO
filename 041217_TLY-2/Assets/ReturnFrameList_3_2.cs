using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFrameList_3_2 : MonoBehaviour {

    private string DisplayID;
    private string PHPFrameUrl = "http://www.pictalive.com/return_framelist.php";
    private string FrameCode;
    public string[] Frame;
    private string[] testing;
    private string qwesdf;

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
        DisplayID = (PlayerPrefs.GetString("userID"));
        StartCoroutine(ReturnFramePHP(DisplayID));

    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator ReturnFramePHP(string MYID)
    {
        GameObject SelectFrameGO = GameObject.Find("FrameChoose");
        SelectFrame selectframescript = SelectFrameGO.GetComponent<SelectFrame>();
        GameObject Deac_chooseGO = GameObject.Find("Manager");
        Deact_choose_Frame deac_chooseScript = Deac_chooseGO.GetComponent<Deact_choose_Frame>();


        WWWForm IDForm = new WWWForm();

        IDForm.AddField("ID_frame", DisplayID);

        WWW sendIDtofile = new WWW(PHPFrameUrl, IDForm);

        yield return sendIDtofile;
        FrameCode = sendIDtofile.text;
        Frame = FrameCode.Split(';');
        Debug.Log(FrameCode);

        /*Debug.Log(Frame[0]);
        Debug.Log(Frame[1]);
        Debug.Log(Frame[2]);
        Debug.Log(Frame[3]);
        Debug.Log(Frame[4]);
        Debug.Log(Frame[5]);*/

        PlayerPrefs.SetString("Frame_1", Frame[0]);
        PlayerPrefs.SetString("Frame_2", Frame[1]);
        PlayerPrefs.SetString("Frame_3", Frame[2]);
        PlayerPrefs.SetString("Frame_4", Frame[3]);
        PlayerPrefs.SetString("Frame_5", Frame[4]);
        PlayerPrefs.SetString("Frame_6", Frame[5]); //URLIDtgt
        PlayerPrefs.SetString("URLIDtgt", FrameCode);

        if (FrameCode == "0;0;0;0;0;0")
        {
            Debug.Log("in 00000!");
            selectframescript.f6.SetActive(true);
            deac_chooseScript.PT_condition = true;
            selectframescript.PT_condition = true;



        }

        else
        {
            deac_chooseScript.PT_condition = false;

            if (Frame[0] == "1")
            {
                ButtonFrame1.SetActive(false);
                Debug.Log("I am here");
            }
            Debug.Log("NOT 00000!");

            if (Frame[1] == "1")
            {
                ButtonFrame2.SetActive(false);
            }

            if (Frame[2] == "1")
            {
                ButtonFrame3.SetActive(false);
            }

            if (Frame[3] == "1")
            {
                ButtonFrame4.SetActive(false);
            }

            if (Frame[4] == "1")
            {
                ButtonFrame5.SetActive(false);
            }
            if (Frame[5] == "1")
            {
                ButtonFrame6.SetActive(false);
            }

        }


    }
}
