using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendDataReg : MonoBehaviour {

    public InputField input;

    void Awake()
    {
        input = GameObject.Find("In_Firstname").GetComponent<InputField>();

    }

    public void GetInput (string Firstname)
    {
        Debug.Log("You entered: " + Firstname);
     


    }

}
