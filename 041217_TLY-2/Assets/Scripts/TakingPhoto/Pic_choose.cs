using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pic_choose : MonoBehaviour {

    public GameObject FrameChoose;
    public GameObject BtnPhoto;
    public GameObject BtnGallery;
    public GameObject BtnFrame;
    public GameObject BtnConfirm;
    public GameObject BtnUpload;
    public GameObject ImgConfirm;
    public GameObject Loading;
    public GameObject DisImage;


    public bool PT_condition = false;

    // Use this for initialization
    void Start() {
        FrameChoose.SetActive(false);
        BtnPhoto.SetActive(true);
        BtnGallery.SetActive(true);
        BtnFrame.SetActive(false);
        BtnConfirm.SetActive(false);
        BtnUpload.SetActive(false);
        Loading.SetActive(false);
        ImgConfirm.SetActive(false);
        DisImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickGallery()
    {
        if (PT_condition == false)
        {
            FrameChoose.SetActive(true);
        }

 
        BtnPhoto.SetActive(true);
        BtnGallery.SetActive(true);
        BtnFrame.SetActive(false);
        BtnConfirm.SetActive(true); // will be activate at FrameChoose <SelectFrame.cs>
        ImgConfirm.SetActive(true); 
        DisImage.SetActive(true);
    }

    public void ClickConfirm()
    {
        FrameChoose.SetActive(false);
        BtnPhoto.SetActive(false);
        BtnGallery.SetActive(false);
        BtnFrame.SetActive(false);
        BtnConfirm.SetActive(true);
        //BtnUpload.SetActive(true);
        ImgConfirm.SetActive(false); // imgUpload will be activate at <ScreenShot.cs>

        StartCoroutine(showUpload());
    }

    IEnumerator showUpload() {
        yield return new WaitForSeconds(2.0f);
        BtnUpload.SetActive(true);
    }

}
