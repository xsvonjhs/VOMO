using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shownusername : MonoBehaviour {

    public Text ShowuN = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowuN.text = (PlayerPrefs.GetString("username"));
    }
}