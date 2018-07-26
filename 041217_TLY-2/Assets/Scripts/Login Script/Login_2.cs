using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Login_2 : MonoBehaviour {

    #region Variable

    //Public
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confPassword;

    //Private
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;

    private string form;
    private bool EmailValid = false; 




    #endregion



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }

        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;


    }
}
