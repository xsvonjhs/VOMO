using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFrame : MonoBehaviour {

    //for 3.1 screenshotAR
    public GameObject Frame1Selected;
    public GameObject Frame2Selected;
    public GameObject Frame3Selected;
    public GameObject Frame4Selected;
    public GameObject Frame5Selected;
    public GameObject Frame6Selected;
    private int SelectedFrame;
    public bool PT_condition = false;

    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;
    public GameObject f5;
    public GameObject f6;
    public GameObject BtnFrameExit;
    public GameObject BtnFrameEnter;

    //only for 5.1 PIC_choose
    public GameObject BtnConfirm;


    // button, if used then deactivate them



    // Use this for initialization
    void Start () {
        f1.SetActive(false);
        f2.SetActive(false);
        f3.SetActive(false);
        f4.SetActive(false);
        f5.SetActive(false);
        f6.SetActive(false);
        BtnConfirm.SetActive(false);
    }
	
	// Update is called once per frame


	void Update ()
    {



        if (SelectedFrame == 1)
        {
            Frame1Selected.SetActive(true);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(false);
            f1.SetActive(true);
            f2.SetActive(false);
            f3.SetActive(false);
            f4.SetActive(false);
            f5.SetActive(false);
            f6.SetActive(false);
            BtnConfirm.SetActive(true);
        }

        else if (SelectedFrame == 2)
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(true);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(false);
            f1.SetActive(false);
            f2.SetActive(true);
            f3.SetActive(false);
            f4.SetActive(false);
            f5.SetActive(false);
            f6.SetActive(false);
            BtnConfirm.SetActive(true);
        }

        else if (SelectedFrame == 3)
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(true);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(false);
            f1.SetActive(false);
            f2.SetActive(false);
            f3.SetActive(true);
            f4.SetActive(false);
            f5.SetActive(false);
            f6.SetActive(false);
            BtnConfirm.SetActive(true);
        }

        else if (SelectedFrame == 4)
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(true);
            Frame5Selected.SetActive(false);
            f1.SetActive(false);
            f2.SetActive(false);
            f3.SetActive(false);
            f4.SetActive(true);
            f5.SetActive(false);
            f6.SetActive(false);
            BtnConfirm.SetActive(true);
        }

        else if (SelectedFrame == 5)
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(true);
            f1.SetActive(false);
            f2.SetActive(false);
            f3.SetActive(false);
            f4.SetActive(false);
            f5.SetActive(true);
            f6.SetActive(false);
            BtnConfirm.SetActive(true);
        }

        else if (SelectedFrame == 6)
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(true);
            f1.SetActive(false);
            f2.SetActive(false);
            f3.SetActive(false);
            f4.SetActive(false);
            f5.SetActive(false);
            f6.SetActive(true);
            BtnConfirm.SetActive(true);
        }

        else
        {
            Frame1Selected.SetActive(false);
            Frame2Selected.SetActive(false);
            Frame3Selected.SetActive(false);
            Frame4Selected.SetActive(false);
            Frame5Selected.SetActive(false);
            BtnConfirm.SetActive(false);
        }

        if (SelectedFrame > 0)
        {
            if (PT_condition == true)
            {
                BtnFrameExit.SetActive(false);
                BtnFrameEnter.SetActive(false);
            }

            else
            {
                BtnFrameExit.SetActive(true);
                //BtnFrameEnter.SetActive(true);
            }

        }
        else
        {
            BtnFrameExit.SetActive(false); BtnFrameEnter.SetActive(false);
        }

    }



    public void SelectedFrame1()
    {
        SelectedFrame = 1;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }

    public void SelectedFrame2()
    {
        SelectedFrame = 2;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }

    public void SelectedFrame3()
    {
        SelectedFrame = 3;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }

   public void SelectedFrame4()
    {
        SelectedFrame = 4;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }

    public void SelectedFrame5()
    {
        SelectedFrame = 5;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }

    public void SelectedFrame6()
    {
        SelectedFrame = 6;
        PlayerPrefs.SetInt("FrameNumber", SelectedFrame);
    }




}
