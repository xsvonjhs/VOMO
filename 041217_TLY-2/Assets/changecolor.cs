using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changecolor : MonoBehaviour {

    public RawImage ChangecolorIMG;
    bool clicked = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }


    public void ChangeColor()
    {
        if (clicked == false)
        {
            clicked = true;
        ChangecolorIMG = GetComponent<RawImage>();
        var tempColor = ChangecolorIMG.color;
        tempColor.a = 1f;
        tempColor.r =0f;
        tempColor.g = 0f;
        tempColor.b = 0f;
        ChangecolorIMG.color = tempColor;

        }

        else
        {
            clicked = false;
            ChangecolorIMG = GetComponent<RawImage>();
            var tempColor = ChangecolorIMG.color;
            tempColor.a = 1f;
            tempColor.r = 1f;
            tempColor.g = 1f;
            tempColor.b = 1f;
            ChangecolorIMG.color = tempColor;
        }
    }
}
