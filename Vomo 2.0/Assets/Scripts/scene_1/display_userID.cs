using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display_userID : MonoBehaviour {

    public Text DisplayIDtxt;
    public GameObject getIDfrom;
    private string IDString;


	// Use this for initialization
	void Start () {

        IDString = (PlayerPrefs.GetString("userID"));
        print(IDString);
        DisplayIDtxt.text = IDString;
		
	}
	
	// Update is called once per frame
	void Update () {
 
    }
}
