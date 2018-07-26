using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortFrameCode : MonoBehaviour {

    // String to store frameCode
    private string[] FrameCode;

    // Use this for initialization
    void Start () {

        FrameCode[1] = (PlayerPrefs.GetString("Frame_1"));
        Debug.Log(FrameCode[1]);

       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
